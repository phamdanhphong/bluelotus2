﻿using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Xml;
using System.Web;

public delegate void ExportExcelEventHandler();

public partial class cms_admin_CommonControls_DownloadExcelFile : System.Web.UI.UserControl
{
    private DataSet dsSource = new DataSet();
    public DataSet DSSource
    {
        set { dsSource = value; }
        get { return dsSource; }
    }

    private event ExportExcelEventHandler export;
    public event ExportExcelEventHandler Export
    {
        add { export += value; }
        remove { export += null; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btOK_Click(object sender, EventArgs e)
    {
        if (export != null)
        {
            export();
            DateTime datetime = DateTime.Now;
            string fileName = "ExportExcel_" + datetime.ToShortDateString() + "_" + datetime.Hour + "_" + datetime.Minute + "_" + datetime.Second;
            ConvertToExcel(dsSource, fileName);            
        }
    }
    

    public void ConvertToExcel(DataSet ds, string fileName)
    {
        ConvertToExcel(ds.Tables, fileName);
    }
    public void ConvertToExcel(IEnumerable tables, string fileName)
    {
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Buffer = true;
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition",
                 "attachment; filename=" + fileName + ".xls");

        using (XmlTextWriter x = new XmlTextWriter(Response.OutputStream, Encoding.UTF8))
        {
            int sheetNumber = 0;
            x.WriteRaw("<?xml version=\"1.0\"?><?mso-application progid=\"Excel.Sheet\"?>");
            x.WriteRaw("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\" ");
            x.WriteRaw("xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
            x.WriteRaw("xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            x.WriteRaw("<Styles><Style ss:ID='sText'>" +
                       "<NumberFormat ss:Format='@'/></Style>");
            x.WriteRaw("<Style ss:ID='sDate'><NumberFormat" +
                       " ss:Format='[$-409]m/d/yy\\ h:mm\\ AM/PM;@'/>");
            x.WriteRaw("</Style></Styles>");
            foreach (DataTable dt in tables)
            {
                sheetNumber++;
                string sheetName = !string.IsNullOrEmpty(dt.TableName) ?
                       dt.TableName : "Sheet" + sheetNumber.ToString();
                x.WriteRaw("<Worksheet ss:Name='" + sheetName + "'>");
                x.WriteRaw("<Table>");
                string[] columnTypes = new string[dt.Columns.Count];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    string colType = dt.Columns[i].DataType.ToString().ToLower();

                    if (colType.Contains("datetime"))
                    {
                        columnTypes[i] = "DateTime";
                        x.WriteRaw("<Column ss:StyleID='sDate'/>");

                    }
                    else if (colType.Contains("string"))
                    {
                        columnTypes[i] = "String";
                        x.WriteRaw("<Column ss:StyleID='sText'/>");

                    }
                    else
                    {
                        x.WriteRaw("<Column />");

                        if (colType.Contains("boolean"))
                        {
                            columnTypes[i] = "Boolean";
                        }
                        else
                        {
                            //default is some kind of number.
                            columnTypes[i] = "Number";
                        }

                    }
                }
                //column headers
                x.WriteRaw("<Row>");
                foreach (DataColumn col in dt.Columns)
                {
                    x.WriteRaw("<Cell ss:StyleID='sText'><Data ss:Type='String'>");
                    x.WriteRaw(col.ColumnName);
                    x.WriteRaw("</Data></Cell>");
                }
                x.WriteRaw("</Row>");
                //data
                bool missedNullColumn = false;
                foreach (DataRow row in dt.Rows)
                {
                    x.WriteRaw("<Row>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (!row.IsNull(i))
                        {
                            if (missedNullColumn)
                            {
                                int displayIndex = i + 1;
                                x.WriteRaw("<Cell ss:Index='" + displayIndex.ToString() +
                                           "'><Data ss:Type='" +
                                           columnTypes[i] + "'>");
                                missedNullColumn = false;
                            }
                            else
                            {
                                x.WriteRaw("<Cell><Data ss:Type='" +
                                           columnTypes[i] + "'>");
                            }

                            switch (columnTypes[i])
                            {
                                case "DateTime":
                                    x.WriteRaw(((DateTime)row[i]).ToString("s"));
                                    break;
                                case "Boolean":
                                    x.WriteRaw(((bool)row[i]) ? "1" : "0");
                                    break;
                                case "String":
                                    x.WriteString(row[i].ToString());
                                    break;
                                default:
                                    x.WriteString(row[i].ToString());
                                    break;
                            }

                            x.WriteRaw("</Data></Cell>");
                        }
                        else
                        {
                            missedNullColumn = true;
                        }
                    }
                    x.WriteRaw("</Row>");
                }
                x.WriteRaw("</Table></Worksheet>");
            }
            x.WriteRaw("</Workbook>");
        }
        Response.End();
    }



    
}
