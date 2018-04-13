
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
            var lProcessedPages = new HashSet<uint>();

            try
            {
                HeaderPage headerPage;
                if (!GetValidPage(0, out headerPage))
                    return false;

                lProcessedPages.Add(0);

                foreach (var pageID in headerPage.CollectionPages.Values)
                {
                    CollectionPage collisionPage;
                    if (!GetValidPage(pageID, out collisionPage))
                        return false;
                    lProcessedPages.Add(pageID);
                    
                    for (var i = 0; i < collisionPage.Indexes.Length; i++)
                    {
                        var collectionIndex = collisionPage.Indexes[i];
                        if (collectionIndex.HeadNode.IsEmpty)
                            continue;

                        IndexPage indexPage;
                        if (!GetValidPage(collectionIndex.HeadNode.PageID, out indexPage))
                            return false;

                        lProcessedPages.Add(collectionIndex.HeadNode.PageID);

                        for (;;)
                        {
                            //if (indexPage.PrevPageID != uint.MaxValue)
                            //   throw new Exception();


                            foreach (var indexNode in indexPage.Nodes.Values)
                            {
                                // indexNode.NextNode / PrevNode

                                // 

                                if (indexNode.DataBlock.IsEmpty)
                                    continue;

                                DataPage dataPage;
                                if (!GetValidPage(indexNode.DataBlock.PageID, out dataPage))
                                    return false;

                                lProcessedPages.Add(indexNode.DataBlock.PageID);

                                foreach (var extend in dataPage.DataBlocks.Values)
                                {
                                    if (extend.ExtendPageID == uint.MaxValue)
                                        continue;

                                    if (!_pager.ValidPage<ExtendPage>(extend.ExtendPageID))
                                        return false;

                                    lProcessedPages.Add(extend.ExtendPageID);
                                }
                            }

                            if (indexPage.NextPageID == uint.MaxValue)
                                break;

                            {
                                uint nextPageID = indexPage.NextPageID;

                                if (!GetValidPage(nextPageID, out indexPage))
                                    return false;

                                lProcessedPages.Add(nextPageID);
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