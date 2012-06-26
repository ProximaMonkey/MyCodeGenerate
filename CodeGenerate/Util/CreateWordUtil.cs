using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using CodeGenerate.Control;
using Word;


namespace CodeGenerate.Util
{
    class CreateWordUtil
    {
        /// <summary>
        /// Create Word Document
        /// </summary>
        /// <param name="ds">Every DataTable in DataSet Generate a table</param>
        /// <param name="dbType"> </param>
        /// <returns></returns>
        public static string Create(DataSet ds, EnumDbType dbType)
        {
            string msg;

            Object nothing = Missing.Value;
       
            Application wordApp = new ApplicationClass();
            wordApp.NormalTemplate.Saved = true; //Avoid PopUp normal.dot Dialog

            Document wordDoc = wordApp.Documents.Add(ref nothing, ref nothing, ref nothing, ref nothing);

            object filePath = @"C:\" + DateTime.Now.ToShortDateString().Replace("/", "") +
                              DateTime.Now.ToLongTimeString().Replace(":", "") + ".doc"; //Saved Path
            try
            {
                int dtCount = 1;
                foreach (DataTable dt in ds.Tables)
                {
                    string[] strTable = dt.TableName.Split('$');
                    string tableName = strTable[0];
                    string tableComments = strTable[1];

                    int rowCount = dt.Rows.Count;
                    List<string> primaryKeys = TableControl.GetPrimayKeys(tableName.ToUpper(), dbType); //Get Table Primary Key

                    //WordDoc.Range(2, 2).InsertParagraphAfter(); //insert return
                    wordApp.Selection.ParagraphFormat.LineSpacing = 15f; //设置文档的行间距
                    ////移动焦点并换行
                    object count = 14;
                    object wdLine = WdUnits.wdLine; //换一行;
                    wordApp.Selection.MoveDown(ref wdLine, ref count, ref nothing); //移动焦点
                    wordApp.Selection.TypeParagraph(); //插入段落(每个表格间增加一行),可视为换行

                    //添加表名称(表格外)
                    wordApp.Selection.TypeText("表" + Convert.ToString(dtCount) + ":" + tableName + "(" + tableComments +
                                               ")");

                    //停留1000毫秒，否则创建表格时会报异常
                    Thread.Sleep(1000);
                    //文档中创建表格( +2表示：表名行和列头行）
                    //5表示列数量
                    Table newTable = wordDoc.Tables.Add(wordApp.Selection.Range, rowCount + 2, 5, ref nothing,
                                                        ref nothing);
                    //设置表格样式
                    //newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleThickThinLargeGap;
                    newTable.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
                    newTable.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
                    newTable.Columns[1].Width = 50f; //序号
                    newTable.Columns[2].Width = 100f; //列名
                    newTable.Columns[3].Width = 100f; //数据类型
                    newTable.Columns[4].Width = 100f; //主键
                    newTable.Columns[5].Width = 100f; //说明

                    #region 填充表名称(表格内)

                    newTable.Cell(1, 1).Range.Text = "表名：" + tableName + "(" + tableComments + ")";
                    newTable.Cell(1, 1).Range.Bold = 2; //设置单元格中字体为粗体
                    //合并单元格
                    newTable.Cell(1, 1).Merge(newTable.Cell(1, 5));
                    newTable.Cell(1, 1).Range.Font.Color = WdColor.wdColorRed; //设置单元格内字体颜色
                    newTable.Cell(1, 1).Select(); //选中
                    wordApp.Selection.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //垂直居中
                    wordApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; //水平居中

                    #endregion

                    #region 填充列标题

                    newTable.Cell(2, 1).Range.Text = "序号";
                    newTable.Cell(2, 1).Range.Font.Color = WdColor.wdColorBlack; //设置单元格内字体颜色
                    newTable.Cell(2, 2).Range.Font.Bold = 2;
                    newTable.Cell(2, 2).Range.Font.Size = 9;
                    newTable.Cell(2, 2).Select(); //选中
                    newTable.Cell(2, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                    newTable.Cell(2, 2).Range.Text = "列名";
                    newTable.Cell(2, 2).Range.Font.Color = WdColor.wdColorBlack; //设置单元格内字体颜色
                    newTable.Cell(2, 2).Range.Font.Bold = 2;
                    newTable.Cell(2, 2).Range.Font.Size = 9;
                    newTable.Cell(2, 2).Select(); //选中
                    newTable.Cell(2, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                    newTable.Cell(2, 3).Range.Text = "数据类型";
                    newTable.Cell(2, 3).Range.Font.Color = WdColor.wdColorBlack; //设置单元格内字体颜色
                    newTable.Cell(2, 3).Range.Font.Bold = 2;
                    newTable.Cell(2, 3).Range.Font.Size = 9;
                    newTable.Cell(2, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(2, 3).Select(); //选中
                    //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                    newTable.Cell(2, 4).Range.Text = "主键";
                    newTable.Cell(2, 4).Range.Font.Color = WdColor.wdColorBlack; //设置单元格内字体颜色
                    newTable.Cell(2, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(2, 4).Range.Font.Bold = 2;
                    newTable.Cell(2, 4).Range.Font.Size = 9;
                    newTable.Cell(2, 4).Select(); //选中
                    //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                    newTable.Cell(2, 5).Range.Text = "说明";
                    newTable.Cell(2, 5).Range.Font.Color = WdColor.wdColorBlack; //设置单元格内字体颜色
                    newTable.Cell(2, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    newTable.Cell(2, 5).Range.Font.Bold = 2;
                    newTable.Cell(2, 5).Range.Font.Size = 9;
                    newTable.Cell(2, 5).Select(); //选中
                    //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居中

                    #endregion

                    #region 填充表格内容

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string dataType = dt.Rows[i]["Data_Type"].ToString(); //数据类型
                        string dataPrecision = dt.Rows[i]["Data_Precision"].ToString(); //精度
                        string dataLength = dt.Rows[i]["Data_Length"].ToString(); //长度
                        string dataScale = dt.Rows[i]["Data_Scale"].ToString(); //
                        string type;
                        if (dataPrecision == string.Empty) //字符串、日期等
                        {
                            if (dataType == "DATE")
                            {
                                type = dataType + "()";
                            }
                            else
                            {
                                type = dataType + "(" + dataLength + ")";
                            }
                        }
                        else //数字类型等
                        {
                            if (dataScale == "0")
                            {
                                type = dataType + "(" + dataPrecision + ")";
                            }
                            else
                            {
                                type = dataType + "(" + dataPrecision + "," + dataScale + ")";
                            }
                        }

                        //序号
                        newTable.Cell(i + 3, 1).Range.Text = Convert.ToString(i + 1);
                        newTable.Cell(i + 3, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                        //列名
                        string pascalColName = dt.Rows[i]["PascalName"].ToString();
                        string columnName = dt.Rows[i]["Column_Name"].ToString();
                        newTable.Cell(i + 3, 2).Range.Text = pascalColName == string.Empty ? columnName : pascalColName;
                        newTable.Cell(i + 3, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居左

                        //数据类型
                        newTable.Cell(i + 3, 3).Range.Text = type;
                        newTable.Cell(i + 3, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居左

                        //主键
                        newTable.Cell(i + 3, 4).Range.Text = primaryKeys.Contains(dt.Rows[i]["Column_Name"].ToString())
                                                                 ? "是"
                                                                 : "";
                        newTable.Cell(i + 3, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居左

                        //说明
                        newTable.Cell(i + 3, 5).Range.Text = dt.Rows[i]["Comments"].ToString();
                        newTable.Cell(i + 3, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //WordApp.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;//水平居左
                    }

                    #endregion

                    //在表格中增加行，注意不是在每个表格间增加行。这就是加了这行代码后为什么每个表格会多出空白行的原因
                    //WordDoc.Content.Tables[dtCount].Rows.Add(ref Nothing);

                    //WordDoc.Paragraphs.Last.Range.Text = "文档创建时间：" + DateTime.Now.ToString();//“落款”
                    //WordDoc.Paragraphs.Last.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    //WordDoc.Content.Tables[dtCount].Rows.Add(ref Nothing);
                    dtCount++;

                    #region 插入分页（每一个表格在一页中显示）

                    //object mymissing = System.Reflection.Missing.Value;
                    //object myunit = Word.WdUnits.wdStory;
                    //WordApp.Selection.EndKey(ref myunit, ref mymissing);
                    //object pBreak = (int)Word.WdBreakType.wdPageBreak;
                    //WordApp.Selection.InsertBreak(ref pBreak);

                    #endregion
                }


                msg = "OK";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            finally
            {
                //文件保存
                wordDoc.SaveAs(ref filePath, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing,
                               ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing, ref nothing,
                               ref nothing, ref nothing, ref nothing);
                wordDoc.Close(ref nothing, ref nothing, ref nothing);
                wordApp.Quit(ref nothing, ref nothing, ref nothing);
                //wordtype.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, appclass, null);//退出

                Marshal.ReleaseComObject(wordDoc);

                Marshal.ReleaseComObject(wordApp);
                GC.Collect();
            }
            Process.Start(filePath.ToString());

            return msg;
        }
    }
}
