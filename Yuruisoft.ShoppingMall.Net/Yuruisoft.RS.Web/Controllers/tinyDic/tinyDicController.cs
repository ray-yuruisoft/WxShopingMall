using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Yuruisoft.RS.Model;

namespace Yuruisoft.RS.Web.Controllers.tinyDic
{
    public class tinyDicController : Controller
    {
        private DbContext Db;
        public tinyDicController()//构造注入
        {
            Db = Yuruisoft.RS.Model.TinyDicDBFactory.TinyDicDBFactory.CreateDbContext();
        }
        [HttpPost]
        public ActionResult Search(string Searchdata, string SeletData)
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            if (Searchdata != null)
            {
                dynamic CurrentS;
                if (SeletData[0] == 'A')
                {
                    #region 1、多表多条件判断，精确查询A部
                    if (SeletData == "A-b")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_b>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-c")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_c>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-d")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_d>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-e")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_e>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-f")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_f>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-g")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_g>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-h")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_h>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-ijk")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_ijk>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-l")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_l>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-m")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_m>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-n")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_n>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-op")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_op>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-qr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_qr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-s")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_s>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-t")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_t>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "A-uvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_A_uvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'B')
                {
                    #region 1、多表多条件判断，精确查询B部
                    if (SeletData == "B-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-jklmn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_jklmn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-o")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_o>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-stu")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_stu>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "B-vwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_B_vwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'C')
                {
                    #region 1、多表多条件判断，精确查询C部
                    if (SeletData == "C-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-bcdefg")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_bcdefg>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-h")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_h>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-ijk")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_ijk>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-lmn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_lmn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-o")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_o>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "C-stuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_C_stuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'D')
                {
                    #region 1、多表多条件判断，精确查询D部
                    if (SeletData == "D-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "D-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "D-i")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_i>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "D-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "D-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "D-stuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_D_stuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'E')
                {
                    #region 1、多表多条件判断，精确查询E部
                    if (SeletData == "E-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-bc")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_bc>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-defg")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_defg>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-hijkl")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_hijkl>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-m")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_m>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-n")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_n>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-opq")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_opq>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-rs")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_rs>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-tuvw")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_tuvw>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "E-xyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_E_xyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'F')
                {
                    #region 1、多表多条件判断，精确查询F部
                    if (SeletData == "F-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-jkl")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_jkl>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-mno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_mno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "F-stuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_F_stuvwvxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'G')
                {
                    #region 1、多表多条件判断，精确查询G部
                    if (SeletData == "G-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-jkl")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_jkl>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-mno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_mno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "G-stuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_G_stuvwvxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'H')
                {
                    #region 1、多表多条件判断，精确查询H部
                    if (SeletData == "H-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_H_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "H-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_H_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "H-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_H_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "H-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_H_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "H-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_H_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'I')
                {
                    #region 1、多表多条件判断，精确查询I部
                    if (SeletData == "I-abcd")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_I_abcd>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "I-efghijklm")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_I_efghijklm>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "I-n")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_I_n>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "I-opqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_I_opqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'J')
                {
                    #region 1、多表多条件判断，精确查询J部

                    CurrentS = Db.Set<WxDic_EnToCn_J>().Where(c => c.WKey == Searchdata);

                    #endregion
                }
                else if (SeletData[0] == 'K')
                {
                    #region 1、多表多条件判断，精确查询K部

                    CurrentS = Db.Set<WxDic_EnToCn_K>().Where(c => c.WKey == Searchdata);

                    #endregion
                }
                else if (SeletData[0] == 'L')
                {
                    #region 1、多表多条件判断，精确查询L部
                    if (SeletData == "L-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_L_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "L-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_L_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "L-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_L_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "L-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_L_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "L-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_L_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'M')
                {
                    #region 1、多表多条件判断，精确查询M部
                    if (SeletData == "M-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_M_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "M-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_M_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "M-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_M_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "M-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_M_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "M-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_M_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'N')
                {
                    #region 1、多表多条件判断，精确查询N部
                    if (SeletData == "N-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_N_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "N-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_N_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "N-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_N_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "N-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_N_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "N-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_N_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'O')
                {
                    #region 1、多表多条件判断，精确查询O部
                    if (SeletData == "O-ab")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_ab>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-cdef")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_cdef>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-ghijklmn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_ghijklmn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-op")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_op>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-qr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_qr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-stu")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_stu>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "O-vwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_O_vwvxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'P')
                {
                    #region 1、多表多条件判断，精确查询P部
                    if (SeletData == "P-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-fgh")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_fgh>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-i")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_i>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-jkl")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_jkl>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-mno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_mno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "P-stuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_P_stuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'Q')
                {
                    #region 1、多表多条件判断，精确查询Q部
                    CurrentS = Db.Set<WxDic_EnToCn_Q>().Where(c => c.WKey == Searchdata);
                    #endregion
                }
                else if (SeletData[0] == 'R')
                {
                    #region 1、多表多条件判断，精确查询R部
                    if (SeletData == "R-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_R_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "R-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_R_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "R-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_R_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "R-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_R_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "R-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_R_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'S')
                {
                    #region 1、多表多条件判断，精确查询S部
                    if (SeletData == "S-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-bc")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_bc>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-de")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_de>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-fgh")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_fgh>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-i")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_i>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-jk")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_jk>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-l")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_l>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-mn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_mn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-o")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_o>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-p")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_p>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-qst")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_qst>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-u")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_u>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "S-vwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_S_vwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'T')
                {
                    #region 1、多表多条件判断，精确查询T部
                    if (SeletData == "T-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-fgh")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_fgh>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-i")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_i>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-jklmn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_jklmn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-o")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_o>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-pqr")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_pqr>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-stu")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_stu>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "T-vwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_T_vwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'U')
                {
                    #region 1、多表多条件判断，精确查询U部
                    if (SeletData == "U-abcdefghijklmn")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_U_abcdefghijklmn>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "U-opqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_U_opqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'V')
                {
                    #region 1、多表多条件判断，精确查询V部
                    if (SeletData == "V-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_V_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "V-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_V_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "V-fghi")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_V_fghi>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "V-jklmnopqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_V_jklmnopqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'W')
                {
                    #region 1、多表多条件判断，精确查询W部
                    if (SeletData == "W-a")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_a>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "W-bcde")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_bcde>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "W-fgh")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_fgh>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "W-i")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_i>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "W-jklmno")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_jklmno>().Where(c => c.WKey == Searchdata);
                    }
                    else if (SeletData == "W-pqrstuvwxyz")
                    {
                        CurrentS = Db.Set<WxDic_EnToCn_W_pqrstuvwxyz>().Where(c => c.WKey == Searchdata);
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'X')
                {
                    #region 1、多表多条件判断，精确查询X部
                    CurrentS = Db.Set<WxDic_EnToCn_X>().Where(c => c.WKey == Searchdata);
                    #endregion
                }
                else if (SeletData[0] == 'Y')
                {
                    #region 1、多表多条件判断，精确查询Y部
                    CurrentS = Db.Set<WxDic_EnToCn_Y>().Where(c => c.WKey == Searchdata);
                    #endregion
                }
                else if (SeletData[0] == 'Z')
                {
                    #region 1、多表多条件判断，精确查询Z部
                    CurrentS = Db.Set<WxDic_EnToCn_Z>().Where(c => c.WKey == Searchdata);
                    #endregion
                }
                else
                {
                    return Json(new { error = true });
                }
                ArrayList SearchDic = new ArrayList();
                foreach (var item in CurrentS)
                {
                    SearchDic.Add(new {
                        id = item.id,
                        Key = item.WKey,
                        Value=item.WValue
                    });
                }
                if (SearchDic.Count != 0)
                    return Json(SearchDic);
                else
                {
                   dynamic CurrentM;
                   if (SeletData[0] == 'A')
                   {
                       #region 2、多表多条件判断，模糊查询A部
                       if (SeletData == "A-b")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_b>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-c")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_c>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-d")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_d>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-e")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_e>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-f")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_f>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-g")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_g>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-h")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_h>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-ijk")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_ijk>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-l")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_l>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-m")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_m>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-n")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_n>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-op")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_op>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-qr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_qr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-s")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_s>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-t")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_t>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "A-uvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_A_uvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'B')
                   {
                       #region 2、多表多条件判断，模糊查询B部
                       if (SeletData == "B-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-jklmn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_jklmn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-o")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_o>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-stu")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_stu>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "B-vwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_B_vwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'C')
                   {
                       #region 2、多表多条件判断，模糊查询C部
                       if (SeletData == "C-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-bcdefg")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_bcdefg>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-h")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_h>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-ijk")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_ijk>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-lmn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_lmn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-o")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_o>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "C-stuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_C_stuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'D')
                   {
                       #region 2、多表多条件判断，模糊查询D部
                       if (SeletData == "D-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "D-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "D-i")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_i>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "D-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "D-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "D-stuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_D_stuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'E')
                   {
                       #region 2、多表多条件判断，模糊查询E部
                       if (SeletData == "E-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-bc")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_bc>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-defg")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_defg>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-hijkl")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_hijkl>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-m")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_m>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-n")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_n>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-opq")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_opq>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-rs")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_rs>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-tuvw")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_tuvw>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "E-xyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_E_xyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'F')
                   {
                       #region 2、多表多条件判断，模糊查询F部
                       if (SeletData == "F-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-jkl")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_jkl>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-mno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_mno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "F-stuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_F_stuvwvxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'G')
                   {
                       #region 2、多表多条件判断，模糊查询G部
                       if (SeletData == "G-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-jkl")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_jkl>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-mno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_mno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "G-stuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_G_stuvwvxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'H')
                   {
                       #region 2、多表多条件判断，模糊查询H部
                       if (SeletData == "H-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_H_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "H-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_H_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "H-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_H_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "H-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_H_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "H-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_H_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'I')
                   {
                       #region 2、多表多条件判断，模糊查询I部
                       if (SeletData == "I-abcd")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_I_abcd>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "I-efghijklm")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_I_efghijklm>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "I-n")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_I_n>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "I-opqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_I_opqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'J')
                   {
                       #region 2、多表多条件判断，模糊查询J部

                       CurrentM = Db.Set<WxDic_EnToCn_J>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();

                       #endregion
                   }
                   else if (SeletData[0] == 'K')
                   {
                       #region 2、多表多条件判断，模糊查询K部

                       CurrentM = Db.Set<WxDic_EnToCn_K>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();

                       #endregion
                   }
                   else if (SeletData[0] == 'L')
                   {
                       #region 2、多表多条件判断，模糊查询L部
                       if (SeletData == "L-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_L_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "L-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_L_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "L-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_L_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "L-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_L_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "L-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_L_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'M')
                   {
                       #region 2、多表多条件判断，模糊查询M部
                       if (SeletData == "M-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_M_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "M-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_M_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "M-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_M_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "M-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_M_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "M-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_M_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'N')
                   {
                       #region 2、多表多条件判断，模糊查询N部
                       if (SeletData == "N-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_N_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "N-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_N_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "N-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_N_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "N-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_N_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "N-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_N_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'O')
                   {
                       #region 2、多表多条件判断，模糊查询O部
                       if (SeletData == "O-ab")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_ab>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-cdef")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_cdef>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-ghijklmn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_ghijklmn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-op")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_op>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-qr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_qr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-stu")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_stu>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "O-vwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_O_vwvxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'P')
                   {
                       #region 2、多表多条件判断，模糊查询P部
                       if (SeletData == "P-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-fgh")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_fgh>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-i")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_i>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-jkl")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_jkl>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-mno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_mno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "P-stuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_P_stuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'Q')
                   {
                       #region 2、多表多条件判断，模糊查询Q部
                       CurrentM = Db.Set<WxDic_EnToCn_Q>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       #endregion
                   }
                   else if (SeletData[0] == 'R')
                   {
                       #region 2、多表多条件判断，模糊查询R部
                       if (SeletData == "R-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_R_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "R-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_R_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "R-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_R_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "R-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_R_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "R-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_R_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'S')
                   {
                       #region 2、多表多条件判断，模糊查询S部
                       if (SeletData == "S-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-bc")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_bc>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-de")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_de>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-fgh")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_fgh>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-i")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_i>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-jk")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_jk>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-l")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_l>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-mn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_mn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-o")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_o>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-p")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_p>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-qst")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_qst>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-u")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_u>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "S-vwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_S_vwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'T')
                   {
                       #region 2、多表多条件判断，模糊查询T部
                       if (SeletData == "T-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-fgh")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_fgh>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-i")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_i>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-jklmn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_jklmn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-o")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_o>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-pqr")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_pqr>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-stu")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_stu>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "T-vwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_T_vwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'U')
                   {
                       #region 2、多表多条件判断，模糊查询U部
                       if (SeletData == "U-abcdefghijklmn")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_U_abcdefghijklmn>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "U-opqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_U_opqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'V')
                   {
                       #region 2、多表多条件判断，模糊查询V部
                       if (SeletData == "V-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_V_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "V-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_V_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "V-fghi")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_V_fghi>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "V-jklmnopqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_V_jklmnopqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'W')
                   {
                       #region 2、多表多条件判断，模糊查询W部
                       if (SeletData == "W-a")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_a>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "W-bcde")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_bcde>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "W-fgh")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_fgh>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "W-i")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_i>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "W-jklmno")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_jklmno>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else if (SeletData == "W-pqrstuvwxyz")
                       {
                           CurrentM = Db.Set<WxDic_EnToCn_W_pqrstuvwxyz>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       }
                       else
                       {
                           return Json(new { error = true });
                       }
                       #endregion
                   }
                   else if (SeletData[0] == 'X')
                   {
                       #region 2、多表多条件判断，模糊查询X部
                       CurrentM = Db.Set<WxDic_EnToCn_X>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       #endregion
                   }
                   else if (SeletData[0] == 'Y')
                   {
                       #region 2、多表多条件判断，模糊查询Y部
                       CurrentM = Db.Set<WxDic_EnToCn_Y>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       #endregion
                   }
                   else if (SeletData[0] == 'Z')
                   {
                       #region 2、多表多条件判断，模糊查询Z部
                       CurrentM = Db.Set<WxDic_EnToCn_Z>().Where(c => c.WKey.Contains(Searchdata)).FirstOrDefault();
                       #endregion
                   }
                   else
                   {
                       return Json(new { error = true });
                   }

                   SearchDic.Add(new
                   {
                       id = CurrentM.id,
                       Key = CurrentM.WKey,
                       Value = CurrentM.WValue
                   });
                   if (SearchDic.Count != 0)
                       return Json(SearchDic);
                }
            }
            return Json(new
            {
                error = true
            });
        }
        [HttpPost]
        public ActionResult SearchKey(string Searchdata,string SeletData, int TakeNum)
        { 
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            if (Searchdata != null && TakeNum>0 )
            {
                dynamic List;

                if (SeletData[0] == 'A')
                {
                    #region 搜索查询多条件A部
                    if (SeletData == "A-b")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_b>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-c")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_c>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-d")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_d>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-e")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_e>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-f")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_f>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-g")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_g>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-h")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_h>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-ijk")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_ijk>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-l")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_l>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-m")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_m>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-n")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_n>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-op")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_op>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-qr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_qr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-s")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_s>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-t")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_t>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "A-uvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_A_uvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'B')
                {
                    #region 搜索查询多条件B部
                    if (SeletData == "B-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-jklmn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_jklmn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-o")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_o>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-stu")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_stu>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "B-vwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_B_vwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'C')
                {
                    #region 搜索查询多条件C部
                    if (SeletData == "C-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-bcdefg")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_bcdefg>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-h")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_h>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-ijk")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_ijk>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-lmn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_lmn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-o")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_o>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "C-stuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_C_stuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'D')
                {
                    #region 搜索查询多条件D部
                    if (SeletData == "D-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "D-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "D-i")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_i>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "D-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "D-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "D-stuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_D_stuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'E')
                {
                    #region 搜索查询多条件E部
                    if (SeletData == "E-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-bc")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_bc>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-defg")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_defg>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-hijkl")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_hijkl>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-m")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_m>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-n")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_n>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-opq")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_opq>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-rs")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_rs>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-tuvw")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_tuvw>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "E-xyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_E_xyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'F')
                {
                    #region 搜索查询多条件F部
                    if (SeletData == "F-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-jkl")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_jkl>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-mno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_mno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "F-stuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_F_stuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'G')
                {
                    #region 搜索查询多条件G部
                    if (SeletData == "G-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-jkl")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_jkl>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-mno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_mno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "G-stuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_G_stuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'H')
                {
                    #region 搜索查询多条件H部
                    if (SeletData == "H-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_H_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "H-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_H_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "H-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_H_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "H-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_H_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "H-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_H_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'I')
                {
                    #region 搜索查询多条件I部
                    if (SeletData == "I-abcd")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_I_abcd>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "I-efghijklm")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_I_efghijklm>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "I-n")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_I_n>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "I-opqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_I_opqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'J')
                {
                    #region 搜索查询多条件J部
                    
                        List = Db.Set<WxDic_SearchKey_EnToCn_J>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    
                    #endregion
                }
                else if (SeletData[0] == 'K')
                {
                    #region 搜索查询多条件K部

                    List = Db.Set<WxDic_SearchKey_EnToCn_K>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据

                    #endregion
                }
                else if (SeletData[0] == 'L')
                {
                    #region 搜索查询多条件L部
                    if (SeletData == "L-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_L_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "L-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_L_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "L-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_L_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "L-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_L_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "L-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_L_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'M')
                {
                    #region 搜索查询多条件M部
                    if (SeletData == "M-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_M_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "M-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_M_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "M-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_M_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "M-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_M_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "M-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_M_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'N')
                {
                    #region 搜索查询多条件N部
                    if (SeletData == "N-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_N_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "N-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_N_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "N-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_N_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "N-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_N_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "N-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_N_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'O')
                {
                    #region 搜索查询多条件O部
                    if (SeletData == "O-ab")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_ab>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-cdef")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_cdef>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-ghijklmn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_ghijklmn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-op")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_op>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-qr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_qr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-stu")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_stu>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "O-vwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_O_vwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'P')
                {
                    #region 搜索查询多条件P部
                    if (SeletData == "P-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-fgh")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_fgh>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-i")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_i>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-jkl")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_jkl>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-mno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_mno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "P-stuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_P_stuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'Q')
                {
                    #region 搜索查询多条件Q部
                    List = Db.Set<WxDic_SearchKey_EnToCn_Q>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    #endregion
                }
                else if (SeletData[0] == 'R')
                {
                    #region 搜索查询多条件R部
                    if (SeletData == "R-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_R_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "R-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_R_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "R-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_R_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "R-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_R_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "R-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_R_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'S')
                {
                    #region 搜索查询多条件S部
                    if (SeletData == "S-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-bc")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_bc>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-de")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_de>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-fgh")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_fgh>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-i")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_i>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-jk")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_jk>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-l")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_l>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-mn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_mn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-o")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_o>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-p")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_p>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-qst")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_qst>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-u")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_u>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "S-vwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_S_vwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'T')
                {
                    #region 搜索查询多条件T部
                    if (SeletData == "T-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-fgh")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_fgh>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-i")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_i>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-jklmn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_jklmn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-o")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_o>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-pqr")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_pqr>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-stu")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_stu>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "T-vwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_T_vwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'U')
                {
                    #region 搜索查询多条件U部
                    if (SeletData == "U-abcdefghijklmn")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_U_abcdefghijklmn>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "U-opqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_U_opqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'V')
                {
                    #region 搜索查询多条件V部
                    if (SeletData == "V-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_V_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "V-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_V_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "V-fghi")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_V_fghi>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "V-jklmnopqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_V_jklmnopqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'W')
                {
                    #region 搜索查询多条件W部
                    if (SeletData == "W-a")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_a>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "W-bcde")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_bcde>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "W-fgh")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_fgh>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "W-i")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_i>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "W-jklmno")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_jklmno>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else if (SeletData == "W-pqrstuvwxyz")
                    {
                        List = Db.Set<WxDic_SearchKey_EnToCn_W_pqrstuvwxyz>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    }
                    else
                    {
                        return Json(new { error = true });
                    }
                    #endregion
                }
                else if (SeletData[0] == 'X')
                {
                    #region 搜索查询多条件X部
                        List = Db.Set<WxDic_SearchKey_EnToCn_X>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    #endregion
                }
                else if (SeletData[0] == 'Y')
                {
                    #region 搜索查询多条件Y部
                    List = Db.Set<WxDic_SearchKey_EnToCn_Y>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    #endregion
                }
                else if (SeletData[0] == 'Z')
                {
                    #region 搜索查询多条件Z部
                    List = Db.Set<WxDic_SearchKey_EnToCn_Z>().Where(c => c.SKey.Contains(Searchdata)).Take(TakeNum).ToList();//只取前几个数据
                    #endregion
                }
                else
                {
                    return Json(new { error = true });
                }
 
               return Json(List);
            }
            return Json(new { error = true });
        }
        [HttpPost]
        public ActionResult SearchCnToEn(string Searchdata,string SeletData)
        {
            if (Request.Headers["yuruisoft"] != "www.yuruisoft.com")//请求头自定义
            {
                return Content("forbid!");
            }
            if (SeletData != null)
            {
                dynamic CurrentS;
                if (SeletData == "a")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_a>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "b")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_b>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "c")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_c>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "d")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_d>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "e")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_e>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "f")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_f>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "g")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_g>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "h")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_h>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "i")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_i>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "j")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_j>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "k")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_k>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "l")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_l>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "m")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_m>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "n")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_n>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "o")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_o>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "p")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_p>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "q")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_q>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "r")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_r>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "s")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_s>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "t")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_t>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "w")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_w>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "x")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_x>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "y")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_y>().Where(c => c.WKey == Searchdata);
                }
                else if (SeletData == "z")
                {
                    CurrentS = Db.Set<WxDic_CnToEn_z>().Where(c => c.WKey == Searchdata);
                }
                else
                {
                    return Json(new { error = true });
                }

                ArrayList SearchDic = new ArrayList();
                foreach (var item in CurrentS)
                {
                    SearchDic.Add(new
                    {
                        id = item.id,
                        Key = item.WKey,
                        Value = item.WValue
                    });
                }
                if (SearchDic.Count != 0)
                    return Json(SearchDic);
                else
                    return Json(new { error = true });
            }
            return Json(new { error = true });
        }
    }
}
