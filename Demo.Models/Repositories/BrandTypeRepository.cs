using System;
using System.Collections.Generic;
using Demo.Models;

namespace Demo.Repositories
{
    /// <summary>
    /// 品牌頻道 - 產品類別 (第一層)
    /// </summary>
    public class BrandTypeRepository : IBrandTypeRepository
    {
        private PetaPoco.Database _db;
        private string _brandTableName = "tbBrand";
        private string _typeTableName = "tbBrandType";
        private string _subTypeTableName = "tbBrandSubType";
        private string _productTableName = "tbProduct";

        public BrandTypeRepository()
        {
            _db = new PetaPoco.Database("conn");
        }

        /// <summary>
        /// 取得分類的 PetaPoco 資料物件
        /// </summary>
        /// <param name="isBackend">是否用於後台（後台不會加上僅撈出顯示的條件）</param>
        /// <returns></returns>
        IList<DataModel_tbBrandType> IBrandTypeRepository.GetList(bool isBackend)
        {
            string sql = "SELECT * FROM tbBrandType WHERE 1=1 ";

            // 僅撈出顯示的資料
            if (!isBackend)
            {
                sql += " AND sVisible=1";
            }

            sql += " ORDER BY sSortid";

            var data = _db.Fetch<DataModel_tbBrandType>(sql);
            return data;
        }

        /// <summary>
        /// 撈取分類資料
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="isBackend"></param>
        /// <returns></returns>
        DataModel_tbBrandType IBrandTypeRepository.Retrieve(long sID, bool isBackend)
        {
            string sql = "SELECT TOP 1 * FROM {0} WHERE sID=@0";

            // 撈出顯示的資料
            if (!isBackend)
            {
                sql += " AND sVisible=1";
            }

            var data = _db.SingleOrDefault<DataModel_tbBrandType>(string.Format(sql, _typeTableName), sID);
            return data;
        }

        /// <summary>
        /// 取得分類總筆數
        /// </summary>
        /// <param name="isBackend">是否用於後台（後台不會加上僅撈出顯示的條件）</param>
        /// <returns></returns>
        long IBrandTypeRepository.RetrieveTypeCount(bool isBackend)
        {
            string sql = "SELECT COUNT(sID) FROM {0} WHERE 1=1 ";

            // 僅撈出顯示的條件
            if (!isBackend)
            {
                sql += " AND sVisible=1";
            }

            long counter = _db.ExecuteScalar<long>(string.Format(sql, _typeTableName));
            return counter;
        }

        /// <summary>
        /// 交換分類排序
        /// </summary>
        /// <param name="sID">ID</param>
        /// <param name="sDirection">方向</param>
        void IBrandTypeRepository.MoveTypeUpDown(long sID, string sDirection)
        {
            _db.Execute("EXEC procTypeSort @tablename, @inputId, @inputAct",
                        new
                        {
                            tablename = _typeTableName,
                            inputId = sID,
                            inputAct = sDirection
                        });
        }

        /// <summary>
        /// 刪除分類
        /// </summary>
        /// <param name="sTypeID">分類 ID</param>
        void IBrandTypeRepository.Delete(long sTypeID)
        {
            _db.Execute(string.Format("DELETE FROM {0} WHERE sID=@0", _typeTableName), sTypeID);
        }

        /// <summary>
        /// 新增 分類標題
        /// </summary>
        /// <param name="sTitle">分類標題</param>
        /// <param name="ErrorMessage">輸出錯誤訊息</param>
        /// <returns>True / False</returns>
        bool IBrandTypeRepository.InsertData(DataModel_tbBrandType model, out string ErrorMessage)
        {
            ErrorMessage = "";
            if (_db.Execute("EXEC procTypeInsert @tablename, @sCaption, @sVisible", new { 
                                                                                        tablename = _typeTableName, 
                                                                                        sCaption = model.sCaption, 
                                                                                        sVisible = model.sVisible 
                                                                                    }) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 更新 分類標題
        /// </summary>
        /// <param name="sID">ID</param>
        /// <param name="sTitle">分類標題</param>
        /// <param name="ErrorMessage">輸出錯誤訊息</param>
        /// <returns>true / false</returns>
        bool IBrandTypeRepository.UpdateData(DataModel_tbBrandType model, out string ErrorMessage)
        {
            ErrorMessage = "";
            if (_db.Execute(String.Format("UPDATE {0} SET sCaption = @0, sVisible = @1 WHERE sID=@2", _typeTableName), model.sCaption, model.sVisible, model.sID) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}