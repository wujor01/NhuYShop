using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using NhuYShop.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace NhuYShop.DAO
{
    public class MWGDAO
    {
        public void ReadExcelTCC (byte[] bin, string USERS, string filename, bool isMerge)
        {
            MWGModel users = JsonConvert.DeserializeObject<MWGModel>(USERS);
            List<TCCYecCauModel> tCCYecCauList = new List<TCCYecCauModel>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (MemoryStream stream = new MemoryStream(bin))
            using (var package = new ExcelPackage(stream))
            {
                //loop all worksheets
                foreach (ExcelWorksheet worksheet in package.Workbook.Worksheets)
                {
                    //loop all rows
                    for (int i = worksheet.Dimension.Start.Row; i <= worksheet.Dimension.End.Row; i++)
                    {
                        //add the cell data to the List
                        if (i != 1)
                        {
                            //list users
                            TCCJobModel tCCJob = new TCCJobModel();
                            string value1 = worksheet.Cells[i, 1].Value.ToString().Trim();
                            string value2 = worksheet.Cells[i, 2].Value.ToString().Trim();
                            string value3 = worksheet.Cells[i, 3].Value.ToString().Trim();
                            string value4 = worksheet.Cells[i, 4].Value.ToString().Trim();
                            string value5 = worksheet.Cells[i, 5].Value.ToString().Trim();
                            string value6 = worksheet.Cells[i, 6].Value.ToString().Trim();
                            string value7 = worksheet.Cells[i, 7].Value.ToString().Trim();
                            string value8 = worksheet.Cells[i, 8].Value.ToString().Trim();

                            string[] userArray = value1.Substring(value1.IndexOf("_")+1).Split(',','.','-');
                            tCCJob.USERLIST = new List<MWGUserModel>();
                            foreach (var item in userArray)
                            {
                                MWGUserModel user = new MWGUserModel();
                                user = users.Sheet1.Where(x => x.USERID == item.Trim()).FirstOrDefault();
                                if (user == null)
                                            {
                                                user = new MWGUserModel();
                                                user.USERID = item.Trim();
                                                user.FULLNAME = "N/A";
                                                user.PHONGBAN = "N/A";
                                                user.STOREID = "N/A";
                                                user.STORENAME = "N/A";
                                            }
                                tCCJob.USERLIST.Add(user);
                            }
                            tCCJob.JOBID = value1.Substring(0,value1.IndexOf("_"));
                            int value = 0;
                            int.TryParse(value4, out value);
                            tCCJob.VALUE = value;
                            tCCJob.CONTENT = value7;

                            if (tCCYecCauList.Where(x=>x.MAYEUCAU == value8).Count() == 0)
                            {
                                TCCYecCauModel tccYeuCau = new TCCYecCauModel();
                                tccYeuCau.MAPHIEUXUAT = value5;
                                tccYeuCau.MAYEUCAU = value8;
                                tccYeuCau.JOBLIST = new List<TCCJobModel>();
                                tccYeuCau.JOBLIST.Add(tCCJob);
                                tccYeuCau.ERROR = value2;
                                int sumvalue = 0;
                                int.TryParse(value3, out sumvalue);
                                tccYeuCau.SUMVALUE = sumvalue;
                                tccYeuCau.NOTE = value7;
                                tCCYecCauList.Add(tccYeuCau);
                            }
                            else
                            {
                                tCCYecCauList
                                    .Where(x => x.MAYEUCAU == value8)
                                    .FirstOrDefault()
                                    .JOBLIST.Add(tCCJob);
                            }
                        }
                    }
                }
            }

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table  
            //  
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;
            workSheet.Cells[1, 1].Value = "Họ và Tên";
            workSheet.Cells[1, 2].Value = "MSNV";
            workSheet.Cells[1, 3].Value = "Phòng ban/ Bộ phận";
            workSheet.Cells[1, 4].Value = "Siêu thị";
            workSheet.Cells[1, 5].Value = "Lỗi/vi phạm sai quy trình";
            workSheet.Cells[1, 6].Value = "Tổng tiền phạt";
            workSheet.Cells[1, 7].Value = "Số tiền đền bù";
            workSheet.Cells[1, 8].Value = "Ghi chú";
            workSheet.Cells[1, 9].Value = "Mã JOB";
            workSheet.Cells[1, 10].Value = "Nội dung xử lý";
            workSheet.Cells[1, 11].Value = "Mã yêu cầu";
            //Body of table  
            //  
            int recordIndex = 2;
            foreach (var yc in tCCYecCauList)
            {
                foreach (var job in yc.JOBLIST)
                {
                    foreach (var user in job.USERLIST)
                    {
                        if (user.FULLNAME == "N/A")
                        {
                            Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#B7DEE8");
                            workSheet.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            workSheet.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(colFromHex);
                        }
                        workSheet.Cells[recordIndex, 1].Value = user.FULLNAME;
                        int id = 0;
                        if (int.TryParse(user.USERID, out id))
                        {
                            workSheet.Cells[recordIndex, 2].Value = id;
                        }
                        else
                        {
                            workSheet.Cells[recordIndex, 2].Value = user.USERID;
                        }
                        workSheet.Cells[recordIndex, 3].Value = user.PHONGBAN;
                        workSheet.Cells[recordIndex, 4].Value = user.STOREID + "-" + user.STORENAME;
                        workSheet.Cells[recordIndex, 5].Value = yc.ERROR;
                        workSheet.Cells[recordIndex, 7].Value = job.VALUE / job.USERLIST.Count;
                        workSheet.Cells[recordIndex, 9].Value = job.JOBID;
                        workSheet.Cells[recordIndex, 10].Value = job.CONTENT;
                        workSheet.Cells[recordIndex, 11].Value = yc.MAYEUCAU;

                            if (job.USERLIST.IndexOf(user) == 0 && yc.JOBLIST.IndexOf(job) == 0)
                            {
                                int mergerow = 0;
                                foreach (var jobrow in yc.JOBLIST)
                                {
                                    foreach (var item in jobrow.USERLIST)
                                    {
                                        mergerow++;
                                    }
                                }
                                workSheet.Cells[recordIndex, 6].Value = yc.SUMVALUE;
                                workSheet.Cells[recordIndex, 8].Value = yc.MAPHIEUXUAT;
                            if (isMerge)
                            {
                                workSheet.Cells[recordIndex, 8, recordIndex + mergerow - 1, 8].Merge = true;
                                workSheet.Cells[recordIndex, 6, recordIndex + mergerow - 1, 6].Merge = true;
                            }
                        }
                        recordIndex++;
                    }
                }

                
            }
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();
            workSheet.Column(10).AutoFit();
            workSheet.Column(11).AutoFit();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            File.WriteAllBytes(path +"/"+filename, excel.GetAsByteArray());
        }
    }
}
