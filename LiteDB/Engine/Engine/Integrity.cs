
using System;
using System.Collections.Generic;

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
                HeaderPage headerPage;
                if (!GetValidPage(0, out headerPage))
                    return false;
                
                foreach (var pageID in headerPage.CollectionPages.Values)
                {
                    CollectionPage collisionPage;
                    if (!GetValidPage(pageID, out collisionPage))
                        return false;
                    
                    for (var i = 0; i < collisionPage.Indexes.Length; i++)
                    {
                        var collectionIndex = collisionPage.Indexes[i];
                        if (collectionIndex.HeadNode.IsEmpty)
                            continue;

                        IndexPage indexPage;
                        if (!GetValidPage(collectionIndex.HeadNode.PageID, out indexPage))
                            return false;
                        
                        var indexNode = indexPage.Nodes[0];
                        
                        while (!indexNode.NextPrev(0, 1).IsEmpty)
                        {
                            if (!GetValidPage(indexNode.NextPrev(0, 1).PageID, out indexPage))
                                return false;
                            
                            indexNode = indexPage.Nodes[indexNode.NextPrev(0, 1).Index];

                            if (!indexNode.DataBlock.IsEmpty)
                            {
                                DataPage dataPage;
                                if (!GetValidPage(indexNode.DataBlock.PageID, out dataPage))
                                    return false;
                                
                                foreach (var extend in dataPage.DataBlocks.Values)
                                {
                                    if (extend.ExtendPageID == uint.MaxValue)
                                        continue;

                                    if (!_pager.ValidPage<ExtendPage>(extend.ExtendPageID))
                                        return false;
                                }
                            }

                            if (indexNode.IsHeadTail(collectionIndex))
                                break;
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private bool GetValidPage<T>(uint pageID, out T page) 
            where T : BasePage
        {
            bool result = _pager.ValidPage<T>(pageID);

            if (result)
                page = _pager.GetPage<T>(pageID);
            else
                page = null;

            return result;
        }

    }
}