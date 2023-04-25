
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MMS.Models
{
    public class IndexGeneration
    {

        private MMSEntities db = new MMSEntities();

        public string CreatePID(int relID,string serno)
            {
                string PID = "";
                string maxSequence_no = "";
            string firstno= "";
            string lastchar2 ="";
            string lastchar1 = "";
            int lastchar3 = 0;
            string seondchar = "";
                DataSet ods = new DataSet();

                //var PIDIndex1 = (from o in db.Patients let num = db.Patients.Select(i => i.PID).Cast<int>().Max() select num).FirstOrDefault();
            var PIDIndex = from s in db.Patients.Where(p => p.ServiceNo == serno).Where(p => p.RelationshipType == relID) select new { s.PID };
            try
                {
               

                foreach (var item in PIDIndex)
                {
                     firstno = item.PID;
                     lastchar2 = firstno.Substring(firstno.Length-2);
                     lastchar1 = lastchar2.Substring(lastchar2.Length -1);
                     lastchar3 = Convert.ToInt32(lastchar1);
                     seondchar = lastchar2.Substring(0,1);
                    if (item.PID==null) {
                        maxSequence_no = serno + "x" + relID.ToString()+"1";

                    }
                    if (item.PID != null && seondchar == "5")
                    {
                        maxSequence_no = serno + "x" + relID.ToString() + ((lastchar3 + 1).ToString());
                    }
                    else
                    {
                        maxSequence_no = serno + "x" + relID.ToString() + "1";
                    }
                }
                if(string.IsNullOrEmpty(maxSequence_no)) { maxSequence_no = serno +"x"+ relID.ToString()+((lastchar3 + 1).ToString()); }
                

            }
                catch(Exception ex)
                {
                    maxSequence_no = "";
                }

            String rest = Regex.Replace(maxSequence_no.ToString(), "[@,\\.\";'//\\\\/]",String.Empty);
                PID = rest;
                return PID;
            }
            public string CreateVID( int id,string pd)
            {
                

            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Vitals.Where(p => p.PDID == pd).ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pd, "");
            no3 = Convert.ToInt32(no1);
            PDID = pd + (no3 + id + 1);

            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = pd + id + 1;
            }

            return PDID;

        }

        public string CreateVIDWard(int id, string pd)
        {


            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Ward_Vitals.Where(p => p.PDID == pd).ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pd, "");
            no3 = Convert.ToInt32(no1);
            PDID = pd + (no3 + id + 1);

            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = pd + id + 1;
            }

            return PDID;

        }
        public string CreateHID(int id,string pid)
        {
           
            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Hypersensivities.Where(p => p.PID == pid).ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pid, "");
            no3 = Convert.ToInt32(no1);
            PDID = pid + (no3 + id + 1);

            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = pid + id + 1;
            }

            return PDID;
        }
        public string CreatesicID(string id)
        {


            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.SickReports.Where(p => p.svcid == id).ToList().Count;

            no1 = PSIndex.ToString();

            no1 = no1.Replace(id, "");
            no3 = Convert.ToInt32(no1);
            PDID = id + (no3 + 1);
            bool isdup = true;
            int i = 2;
            while (isdup == true)
            {
                var checkpdid = from s in db.SickReports.Where(p => p.PDID == PDID) select new { s.PDID };
                if (checkpdid.Count() > 0)
                {
                    foreach (var item in checkpdid)
                    {
                        if (item.PDID == PDID)
                        {
                            PDID = id + (no3 + i);
                            i++;
                            isdup = true;

                        }
                        else
                        {
                            isdup = false;
                        }
                    }
                }
                else
                {
                    isdup = false;
                }



            }



            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = id + 1;
            }

            return PDID;

        }
        public string Createregno(string id)
        {

            string id1 = "";
            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;

            if (id.Equals("CBO"))
            {
                id1 = "23";
            }
            if (id.Equals("RMA"))
            {
                id1 = "45";
            }
            if (id.Equals("KAT"))
            {
                id1 = "24";
            }
            if (id.Equals("EKA"))
            {
                id1 = "26";
            }
            if (id.Equals("MGR"))
            {
                id1 = "27";
            }
            if (id.Equals("BIA"))
            {
                id1 = "28";
            }
            if (id.Equals("PLV"))
            {
                id1 = "32";
            }
            if (id.Equals("SGR"))
            {
                id1 = "33";
            }
            if (id.Equals("BCL"))
            {
                id1 = "34";
            }
            if (id.Equals("HIN"))
            {
                id1 = "38";
            }
            if (id.Equals("CBY"))
            {
                id1 = "36";
            }
            if (id.Equals("AHP"))
            {
                id1 = "37";
            }
            if (id.Equals("MOW"))
            {
                id1 = "39";
            }
            if (id.Equals("KTK"))
            {
                id1 = "43";
            }
            if (id.Equals("WLA"))
            {
                id1 = "44";
            }
            if (id.Equals("KGL"))
            {
                id1 = "46";
            }
            if (id.Equals("DLA"))
            {
                id1 = "47";
            }
            if (id.Equals("AMP"))
            {
                id1 = "48";
            }
            if (id.Equals("PGL"))
            {
                id1 = "49";
            }
            if (id.Equals("KKS"))
            {
                id1 = "50";
            }
            if (id.Equals("IRM"))
            {
                id1 = "52";
            }
            if (id.Equals("MTV"))
            {
                id1 = "53";
            }
            if (id.Equals("WAN"))
            {
                id1 = "54";
            }
            if (id.Equals("VNA"))
            {
                id1 = "58";
            }

            var PSIndex = db.claim_detail.Where(p => p.loc == id).Where(p => p.created_date.Value.Year == DateTime.Now.Year).ToList().Count;

            no1 = PSIndex.ToString();

            //no1 = no1.Replace(id, "");
            no3 = Convert.ToInt32(no1);
            PDID = DateTime.Now.Year+"/"+ id1  +"/" + (no3 + 1);
            bool isdup = true;
            int i = 2;
            while (isdup == true)
            {
                var checkpdid = from s in db.claim_detail.Where(p => p.RegisterNo == PDID) select new { s.RegisterNo };
                if (checkpdid.Count() > 0)
                {
                    foreach (var item in checkpdid)
                    {
                        if (item.RegisterNo == PDID)
                        {
                            PDID =DateTime.Now.Year+"/" + id1 + "/" + (no3 + i);
                            i++;
                            isdup = true;

                        }
                        else
                        {
                            isdup = false;
                        }
                    }
                }
                else
                {
                    isdup = false;
                }



            }



            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = DateTime.Now.Year + "/" + id1 +"/" + 1;
            }

            return PDID;

        }


        public string CreatePDID(string id)
            {
           

            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Patient_Detail.Where(p => p.PID == id).ToList().Count;
           
            no1 = PSIndex.ToString();

            no1 = no1.Replace(id, "");
            no3 = Convert.ToInt32(no1);
            PDID = id + (no3 + 1);
            bool isdup = true;
            int i = 2;
            while (isdup==true) {
                var checkpdid = from s in db.Patient_Detail.Where(p => p.PDID == PDID) select new { s.PDID };
                if (checkpdid.Count() >0)
                {
                    foreach (var item in checkpdid)
                    {
                        if (item.PDID == PDID)
                        {
                            PDID = id + (no3 + i);
                            i++;
                            isdup = true;

                        }
                        else
                        {
                            isdup = false;
                        }
                    }
                }
                else
                {
                    isdup = false;
                }

                

            }



                    //}
                    if (string.IsNullOrEmpty(PDID))
            {
                PDID = id + 1;
            }

            return PDID;

        }
        public string CreatePSID(int id, string pdid)
        {
            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Drug_Prescription.Where(p => p.PDID==pdid).ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pdid, "");
            no3 = Convert.ToInt32(no1);
            PDID = pdid + (no3 + id + 1);
           
            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID =  pdid + id + 1;
            }

            return PDID;
        }
        public string CreateSCID(int id, string pdid)
        {
            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.Sick_Category.Where(p => p.PDID == pdid).ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pdid, "");
            no3 = Convert.ToInt32(no1);
            PDID = pdid + (no3 + id + 1);

            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = pdid + id + 1;
            }

            return PDID;
        }
        public int CreateSTID()
        {
            string PDID = "";
            int no1 = 0;
            string no2 = "";
            int no3 = 0;
            int PSIndex = db.Staff_Master.ToList().Count;
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex;

           // no1 = no1+1;
            no3 = Convert.ToInt32(no1)+2;
            //  PDID = pdid + (no3 + id + 1);

            //}
            // if (string.IsNullOrEmpty(PDID))
            //// {
            //     PDID = pdid + id + 1;
            // }
            bool isdup = true;
            int i = 2;
            while (isdup == true)
            {
                var checkpdid = from s in db.Staff_Master.Where(p => p.SID == no3) select new { s.SID };
                if (checkpdid.Count() > 0)
                {
                    foreach (var item in checkpdid)
                    {
                        if (item.SID == no3)
                        {
                            no3 =(no3 + i);
                            i++;
                            isdup = true;

                        }
                        else
                        {
                            isdup = false;
                        }
                    }
                }
                else
                {
                    isdup = false;
                }



            }
            return no3;
        }
        public String CreatedrID(int id)
        {
            string PDID = "";
            string no1 = "";
            string no2 = "";
            int no3 = 0;
            var PSIndex = db.DrugStockMasters.ToList().Max(e=>Convert.ToInt64(e.ItemIndex));
            //var PSIndex = from s in db.Drug_Prescription.Where(p => p.Ps_Index.Contains(id)) select new { s.Ps_Index };
            //foreach (var item in PSIndex)
            //{
            no1 = PSIndex.ToString();

           // no1 = no1.Replace(pdid, "");
            no3 = Convert.ToInt32(no1);
            PDID = ( no3 + id + 1).ToString();

            //}
            if (string.IsNullOrEmpty(PDID))
            {
                PDID = ( id + 1).ToString();
            }

            return PDID;
        }
        public string CreateLABID(int id,string pdid)
        {
            string LABID = "";
            string no1 = "";
            int no2 = 0;
            int no3 = 0;
            //var PSIndex = (from s in db.Lab_Report.Where(p => p.Lab_Index.Contains(pdid)) let num = db.Lab_Report.Select(i => i.Lab_Index).Cast<long>().Max() select num).FirstOrDefault();
            var PSIndex =  db.Lab_Report.Where(p => p.PDID==pdid).ToList().Count;
                        //  orderby Convert.ToInt32(s.Lab_Index) select new { s.Lab_Index };
            //foreach (var item in PSIndex)
            //{
                //LABID = PSIndex;
                no1 = PSIndex.ToString();

                no1 = no1.Replace(pdid,"");
                no3 = Convert.ToInt32(no1);
                LABID = pdid + (no3 + id+1);
            //}
            if (string.IsNullOrEmpty(LABID))
            {
                LABID = pdid + id+1;
            }

            return LABID;
        }
        public string CreateTransID(string pdid)
            {
                string TransID = "";
            string no1 = "";
            int no2 = 0;
            int no3 = 0;
            //var PSIndex = (from s in db.Lab_Report.Where(p => p.Lab_Index.Contains(pdid)) let num = db.Lab_Report.Select(i => i.Lab_Index).Cast<long>().Max() select num).FirstOrDefault();
            var PSIndex = db.TranferDetails.Where(p => p.PDID == pdid).ToList().Count;
            //  orderby Convert.ToInt32(s.Lab_Index) select new { s.Lab_Index };
            //foreach (var item in PSIndex)
            //{
            //LABID = PSIndex;
            no1 = PSIndex.ToString();

            no1 = no1.Replace(pdid, "");
            no3 = Convert.ToInt32(no1);
            TransID = pdid + (no3 + 1);
            //}
            if (string.IsNullOrEmpty(TransID))
            {
                TransID = pdid + 1;
            }
            return TransID;
            }

        }
    }
