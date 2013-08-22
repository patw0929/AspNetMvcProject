using System.Collections.Generic;
using Demo.Models;

namespace Demo.Repositories
{
    /// <summary>
    /// 產品類型
    /// </summary>
    public interface IBrandTypeRepository
    {
        /// <summary>
        /// 取得分頁列表
        /// </summary>
        /// <param name="page">所在頁數</param>
        /// <param name="PageSize">每頁筆數</param>
        /// <param name="sCaption">產品類型名稱</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        PetaPoco.Page<DataModel_tbBrandType> GetPagedList(long page, long PageSize, string sCaption, bool isBackend = true);

        /// <summary>
        /// 取得列表
        /// </summary>
        /// <param name="sCaption">產品類型名稱</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        IList<DataModel_tbBrandType> GetList(string sCaption, bool isBackend = true);

        /// <summary>
        /// 取得列表
        /// </summary>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        IList<DataModel_tbBrandType> GetList(bool isBackend = true);

        /// <summary>
        /// 取得產品類型數
        /// </summary>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        long RetrieveTypeCount(bool isBackend = true);

        /// <summary>
        /// 藉由產品類型 ID 取得其下所有次分類數量
        /// </summary>
        /// <param name="sTypeID">產品類型 ID</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        long RetrieveSubTypeCountByType(long sTypeID, bool isBackend = true);

        /// <summary>
        /// 藉由產品類型 ID 取得其下所有品牌數量
        /// </summary>
        /// <param name="sTypeID">產品類型 ID</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        long RetrieveBrandCountByType(long sTypeID, bool isBackend = true);

        /// <summary>
        /// 藉由產品類型 ID 取得其下所有產品數
        /// </summary>
        /// <param name="sTypeID">品牌類型 ID</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        long RetrieveProductCountByType(long sTypeID, bool isBackend = true);

        /// <summary>
        /// 取得資料
        /// </summary>
        /// <param name="sID">ID</param>
        /// <param name="isBackend">是否使用於後台</param>
        /// <returns></returns>
        DataModel_tbBrandType Retrieve(long sID, bool isBackend = true);

        /// <summary>
        /// 切換顯示狀態
        /// </summary>
        /// <param name="sID">ID</param>
        /// <returns></returns>
        bool ToggleVisible(long sID);

        /// <summary>
        /// 上下移動
        /// </summary>
        /// <param name="sID">ID</param>
        /// <param name="sDirection">方向</param>
        void MoveTypeUpDown(long sID, string sDirection);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="sTypeID">產品類型 ID</param>
        void Delete(long sTypeID);

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="model">資料模型</param>
        /// <param name="ErrorMessage">錯誤訊息 (輸出)</param>
        /// <returns></returns>
        bool InsertData(DataModel_tbBrandType model, out string ErrorMessage);

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <param name="model">資料模型</param>
        /// <param name="ErrorMessage">錯誤訊息 (輸出)</param>
        /// <returns></returns>
        bool UpdateData(DataModel_tbBrandType model, out string ErrorMessage);
    }
}