
using System;

namespace LiteDB
{
    public partial class LiteEngine
    {
        /// <summary>
        /// </summary>
        public bool IntegrityCheck()
        {
            bool result = true;
            try
            {
                result &= _pager.ValidPage<HeaderPage>(0);

                if (!result)
                    return result;

                var headerPage = _pager.GetPage<HeaderPage>(0);
                foreach (var pageID in headerPage.CollectionPages.Values)
                {
                    result &= _pager.ValidPage<CollectionPage>(pageID);

                    if (!result)
                        return result;

                    var collisionPage = _pager.GetPage<CollectionPage>(pageID);
                    for (var i = 0; i < collisionPage.Indexes.Length; i++)
                    {
                        var index = collisionPage.Indexes[i];
                        if (index.HeadNode.IsEmpty)
                            continue;

                        result &= _pager.ValidPage<IndexPage>(index.HeadNode.PageID);

                        if (!result)
                            return result;

                        var headNode = _pager.GetPage<IndexPage>(index.HeadNode.PageID);

                        //  loop through indices

                        foreach (var node in headNode.Nodes.Values)
                        {
                            if (node.DataBlock.IsEmpty)
                                continue;

                            result &= _pager.ValidPage<DataPage>(node.DataBlock.PageID);

                            if (!result)
                                return result;

                            var dataPage = _pager.GetPage<DataPage>(node.DataBlock.PageID);
                            foreach (var extend in dataPage.DataBlocks.Values)
                            {
                                if (extend.ExtendPageID == uint.MaxValue)
                                    continue;

                                result &= _pager.ValidPage<ExtendPage>(extend.ExtendPageID);

                                if (!result)
                                    return result;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }
    }
}