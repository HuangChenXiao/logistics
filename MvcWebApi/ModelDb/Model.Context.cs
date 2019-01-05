﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ModelDb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EBMSystemEntities : DbContext
    {
        public EBMSystemEntities()
            : base("name=EBMSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BangDingJiLu> BangDingJiLu { get; set; }
        public virtual DbSet<BaoXianJiLu> BaoXianJiLu { get; set; }
        public virtual DbSet<BaoYangJiLu> BaoYangJiLu { get; set; }
        public virtual DbSet<BaseCode> BaseCode { get; set; }
        public virtual DbSet<CangKuInfo> CangKuInfo { get; set; }
        public virtual DbSet<CheLiangInfo> CheLiangInfo { get; set; }
        public virtual DbSet<CurrentStore> CurrentStore { get; set; }
        public virtual DbSet<GongChengCheDingDan> GongChengCheDingDan { get; set; }
        public virtual DbSet<GongDiCheLiang> GongDiCheLiang { get; set; }
        public virtual DbSet<GongDiInfo> GongDiInfo { get; set; }
        public virtual DbSet<GongDiXianLu> GongDiXianLu { get; set; }
        public virtual DbSet<GradeDef> GradeDef { get; set; }
        public virtual DbSet<JiaYouJiLu> JiaYouJiLu { get; set; }
        public virtual DbSet<KeHuDaLei> KeHuDaLei { get; set; }
        public virtual DbSet<KeHuInfo> KeHuInfo { get; set; }
        public virtual DbSet<NianJianJiLu> NianJianJiLu { get; set; }
        public virtual DbSet<ShangBanLeiBie> ShangBanLeiBie { get; set; }
        public virtual DbSet<ShangPinChuKu> ShangPinChuKu { get; set; }
        public virtual DbSet<ShangPinDaLei> ShangPinDaLei { get; set; }
        public virtual DbSet<ShangPinInfo> ShangPinInfo { get; set; }
        public virtual DbSet<ShangPinRuKu> ShangPinRuKu { get; set; }
        public virtual DbSet<ShangPinRuKuS> ShangPinRuKuS { get; set; }
        public virtual DbSet<ShiGuJiLu> ShiGuJiLu { get; set; }
        public virtual DbSet<StoreRecord> StoreRecord { get; set; }
        public virtual DbSet<Sys_AbleHold> Sys_AbleHold { get; set; }
        public virtual DbSet<Sys_AbleList> Sys_AbleList { get; set; }
        public virtual DbSet<Sys_User> Sys_User { get; set; }
        public virtual DbSet<SystemSet> SystemSet { get; set; }
        public virtual DbSet<TuWeiInfo> TuWeiInfo { get; set; }
        public virtual DbSet<VouchCode> VouchCode { get; set; }
        public virtual DbSet<WaJueJiDingDan> WaJueJiDingDan { get; set; }
        public virtual DbSet<wechatAccessToken> wechatAccessToken { get; set; }
        public virtual DbSet<wechatRole> wechatRole { get; set; }
        public virtual DbSet<wechatTram> wechatTram { get; set; }
        public virtual DbSet<wechatUser> wechatUser { get; set; }
        public virtual DbSet<WeiXiuJiLu> WeiXiuJiLu { get; set; }
        public virtual DbSet<WeiZhangJiLu> WeiZhangJiLu { get; set; }
        public virtual DbSet<XianLuInfo> XianLuInfo { get; set; }
        public virtual DbSet<XieZuoDanWeiDaLei> XieZuoDanWeiDaLei { get; set; }
        public virtual DbSet<XieZuoDanWeiInfo> XieZuoDanWeiInfo { get; set; }
        public virtual DbSet<YeZhuHuiYiJiLu> YeZhuHuiYiJiLu { get; set; }
        public virtual DbSet<YeZhuInfo> YeZhuInfo { get; set; }
        public virtual DbSet<GongChengCheDingDan_View> GongChengCheDingDan_View { get; set; }
        public virtual DbSet<WaJueJiDingDan_View> WaJueJiDingDan_View { get; set; }
    
        public virtual int PD_BaseCode(ObjectParameter newCode, string vouchType)
        {
            var vouchTypeParameter = vouchType != null ?
                new ObjectParameter("VouchType", vouchType) :
                new ObjectParameter("VouchType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PD_BaseCode", newCode, vouchTypeParameter);
        }
    
        public virtual int PD_VouchCode(ObjectParameter newCode, Nullable<System.DateTime> riQi, string vouchType)
        {
            var riQiParameter = riQi.HasValue ?
                new ObjectParameter("RiQi", riQi) :
                new ObjectParameter("RiQi", typeof(System.DateTime));
    
            var vouchTypeParameter = vouchType != null ?
                new ObjectParameter("VouchType", vouchType) :
                new ObjectParameter("VouchType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PD_VouchCode", newCode, riQiParameter, vouchTypeParameter);
        }
    
        public virtual int PD_EndGongChengCheDingDan(string cDingDanHao, string cGongDiBianMa, string cChePaiHao, string js_openid, string cGuanLiYuanBianMa, string cTuWeiBianMa, string begindate, string enddate)
        {
            var cDingDanHaoParameter = cDingDanHao != null ?
                new ObjectParameter("cDingDanHao", cDingDanHao) :
                new ObjectParameter("cDingDanHao", typeof(string));
    
            var cGongDiBianMaParameter = cGongDiBianMa != null ?
                new ObjectParameter("cGongDiBianMa", cGongDiBianMa) :
                new ObjectParameter("cGongDiBianMa", typeof(string));
    
            var cChePaiHaoParameter = cChePaiHao != null ?
                new ObjectParameter("cChePaiHao", cChePaiHao) :
                new ObjectParameter("cChePaiHao", typeof(string));
    
            var js_openidParameter = js_openid != null ?
                new ObjectParameter("js_openid", js_openid) :
                new ObjectParameter("js_openid", typeof(string));
    
            var cGuanLiYuanBianMaParameter = cGuanLiYuanBianMa != null ?
                new ObjectParameter("cGuanLiYuanBianMa", cGuanLiYuanBianMa) :
                new ObjectParameter("cGuanLiYuanBianMa", typeof(string));
    
            var cTuWeiBianMaParameter = cTuWeiBianMa != null ?
                new ObjectParameter("cTuWeiBianMa", cTuWeiBianMa) :
                new ObjectParameter("cTuWeiBianMa", typeof(string));
    
            var begindateParameter = begindate != null ?
                new ObjectParameter("begindate", begindate) :
                new ObjectParameter("begindate", typeof(string));
    
            var enddateParameter = enddate != null ?
                new ObjectParameter("enddate", enddate) :
                new ObjectParameter("enddate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PD_EndGongChengCheDingDan", cDingDanHaoParameter, cGongDiBianMaParameter, cChePaiHaoParameter, js_openidParameter, cGuanLiYuanBianMaParameter, cTuWeiBianMaParameter, begindateParameter, enddateParameter);
        }
    
        public virtual ObjectResult<string> PROC_cXianLuBianMa()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("PROC_cXianLuBianMa");
        }
    
        public virtual ObjectResult<string> PROC_GongDiCode(string vouchType)
        {
            var vouchTypeParameter = vouchType != null ?
                new ObjectParameter("VouchType", vouchType) :
                new ObjectParameter("VouchType", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("PROC_GongDiCode", vouchTypeParameter);
        }
    
        public virtual ObjectResult<PROC_OrderSumCount_Result> PROC_OrderSumCount(string cGongDiMingCheng, string cTuWeiMingCheng, string cXZDWMingCheng, string cXingMing, string cChePaiHao, string begindDanJuRiQi, string enddDanJuRiQi, string cGuanLiYuanBianMa)
        {
            var cGongDiMingChengParameter = cGongDiMingCheng != null ?
                new ObjectParameter("cGongDiMingCheng", cGongDiMingCheng) :
                new ObjectParameter("cGongDiMingCheng", typeof(string));
    
            var cTuWeiMingChengParameter = cTuWeiMingCheng != null ?
                new ObjectParameter("cTuWeiMingCheng", cTuWeiMingCheng) :
                new ObjectParameter("cTuWeiMingCheng", typeof(string));
    
            var cXZDWMingChengParameter = cXZDWMingCheng != null ?
                new ObjectParameter("cXZDWMingCheng", cXZDWMingCheng) :
                new ObjectParameter("cXZDWMingCheng", typeof(string));
    
            var cXingMingParameter = cXingMing != null ?
                new ObjectParameter("cXingMing", cXingMing) :
                new ObjectParameter("cXingMing", typeof(string));
    
            var cChePaiHaoParameter = cChePaiHao != null ?
                new ObjectParameter("cChePaiHao", cChePaiHao) :
                new ObjectParameter("cChePaiHao", typeof(string));
    
            var begindDanJuRiQiParameter = begindDanJuRiQi != null ?
                new ObjectParameter("BegindDanJuRiQi", begindDanJuRiQi) :
                new ObjectParameter("BegindDanJuRiQi", typeof(string));
    
            var enddDanJuRiQiParameter = enddDanJuRiQi != null ?
                new ObjectParameter("EnddDanJuRiQi", enddDanJuRiQi) :
                new ObjectParameter("EnddDanJuRiQi", typeof(string));
    
            var cGuanLiYuanBianMaParameter = cGuanLiYuanBianMa != null ?
                new ObjectParameter("cGuanLiYuanBianMa", cGuanLiYuanBianMa) :
                new ObjectParameter("cGuanLiYuanBianMa", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PROC_OrderSumCount_Result>("PROC_OrderSumCount", cGongDiMingChengParameter, cTuWeiMingChengParameter, cXZDWMingChengParameter, cXingMingParameter, cChePaiHaoParameter, begindDanJuRiQiParameter, enddDanJuRiQiParameter, cGuanLiYuanBianMaParameter);
        }
    }
}
