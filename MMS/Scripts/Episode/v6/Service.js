app.service("EpisodeService", function ($http) {

    //get All Details
    this.getPors = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetAllPOR",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.GetsurgatientDischarj = function (id, id2) {
        debugger;
        var response = $http({
            method: "post",
            url: "/SurgeryMasters/GetsurgatientDischarj",
            params: {
                id: JSON.stringify(id),
                id2: JSON.stringify(id2)
            }
        });
        return response;


    };

    this.SavepatientWardNurse = function (bitems, reftohosp, planofmgt, drughyst, hitems, pastmedhys, items, sitems, litems, ditems, Present_Complain, History_PresentComplain, Other_Complain, History_OtherComplain, AbdOther, PDID, pntstatus, GClinic, dgn1, genex, cardex, cenex, resex, othex, abdex, drugins, remarks, wardNo, bedNo, mgtPlan, BHTNo, psitemsWard) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Ward/SavepatientWardNurse",
            data: $.param({
                reftohosp: JSON.stringify(reftohosp),
                planofmgt: JSON.stringify(planofmgt),
                drughyst: JSON.stringify(drughyst),
                hitems: JSON.stringify(hitems),
                pastmedhys: JSON.stringify(pastmedhys),
                items: JSON.stringify(items),
                sitems: JSON.stringify(sitems),
                litems: JSON.stringify(litems),
                ditems: JSON.stringify(ditems),
                bitems: JSON.stringify(bitems),
                Present_Complain: JSON.stringify(Present_Complain),
                History_PresentComplain: JSON.stringify(History_PresentComplain),
                Other_Complain: JSON.stringify(Other_Complain),
                History_OtherComplain: JSON.stringify(History_OtherComplain),
                AbdOther: JSON.stringify(AbdOther),
                dgn1: JSON.stringify(dgn1),
                PDID: JSON.stringify(PDID),
                genex: JSON.stringify(genex),
                cardex: JSON.stringify(cardex),
                cenex: JSON.stringify(cenex),
                resex: JSON.stringify(resex),
                othex: JSON.stringify(othex),
                abdex: JSON.stringify(abdex),
                drugins: JSON.stringify(drugins),
                pntstatus: JSON.stringify(pntstatus),
                GClinic: JSON.stringify(GClinic),
                remarks: JSON.stringify(remarks),
                wardNo: JSON.stringify(wardNo),
                bedNo: JSON.stringify(bedNo),
                mgtPlan: JSON.stringify(mgtPlan),
                BHTNo: JSON.stringify(BHTNo),
                psitemsWard: JSON.stringify(psitemsWard)
            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.viewlabtest = function (id, catid) {

        var response = $http({
            method: "post",
            url: "/Lab_Report/viewlabtest",
            params: {
                id: JSON.stringify(id),
                catid: JSON.stringify(catid)
            }
        });
        return response;


    };
    this.acctest = function (id, catid) {

        var response = $http({
            method: "post",
            url: "/Lab_Report/acctest",
            params: {
                id: JSON.stringify(id),
                catid: JSON.stringify(catid)
            }
        });
        return response;


    };

    this.Savedo = function (id) {

        var response = $http({
            method: "post",
            url: "/Drug_Prescription/Savedo",
            data: $.param({
                id: JSON.stringify(id)

            }),
             headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.loaddorder = function (id) {

        var response = $http({
            method: "post",
            url: "/Drug_Prescription/loaddorder",
            params: {
                id: JSON.stringify(id),
              
            }
        });
        return response;


    };

    this.saverpcdrug = function (id, rpcdr2) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/saverpcdrug",
            params: {
                id: JSON.stringify(id),
                rpcdr2: JSON.stringify(rpcdr2)

            }
        });
        return response;


    };
    this.submitdoc = function (id, rpcdr2) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/saverpcdrug",
            params: {
                id: JSON.stringify(id),
                rpcdr2: JSON.stringify(rpcdr2)

            }
        });
        return response;


    };
this.delPatient = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/delPatient",
            params: {
                id: JSON.stringify(id)
                

            }
        });
        return response;


    };
    this.delregdrug = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/delregdrug",
            params: {
                id: JSON.stringify(id)


            }
        });
        return response;


    };
this.deldrug = function (id) {

    var response = $http({
        method: "post",
        url: "/Patient_Detail/deldrug",
        params: {
            id: JSON.stringify(id)


        }
    });
    return response;


};

this.deldrugWard = function (id) {

    var response = $http({
        method: "post",
        url: "/Ward/deldrugWard",
        params: {
            id: JSON.stringify(id)
        }
    });
    return response;


};

this.IssuDrug = function (id,id2,id3) {

    var response = $http({
        method: "post",
        url: "/Ward/IssuDrug",
        params: {
            id: JSON.stringify(id),
            Duration: JSON.stringify(id2),
            pdid: JSON.stringify(id3)


        }
    });
    return response;


};

    this.savetrnsdrug = function (id, drugtrqt, Clinic_ID) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/savetrnsdrug",
            params: {
                id: JSON.stringify(id),
                drugtrqt: JSON.stringify(drugtrqt),
                Clinic_ID: JSON.stringify(Clinic_ID)

            }
        });
        return response;


    };
    this.Getdiv = function (id,id1) {

        var response = $http({
            method: "post",
            url: "/Users/Getdiv",
            params: {
                id: JSON.stringify(id),
                id1: JSON.stringify(id1)

            }
        });
        return response;


    };

    this.Getsubmmenu = function (id) {

        var response = $http({
            method: "post",
            url: "/Users/Getsubmmenu",
            params: {
                id: JSON.stringify(id)
               

            }
        });
        return response;


    };
    this.Getclaimmob = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getclaimmob",
            params: {
                id: JSON.stringify(id)
               

            }
        });
        return response;


    };

    this.Getdivnurs = function (id) {

        var response = $http({
            method: "post",
            url: "/Users/Getdivnurs",
            params: {
                id: JSON.stringify(id)
               

            }
        });
        return response;


    };
    this.Getdrugonr = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/Getdrugonr",
            params: {
                id: JSON.stringify(id)
              

            }
        });
        return response;


    };
    this.Getdrugalldep = function (id) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/Getdrugalldep",
            params: {
                id: JSON.stringify(id)


            }
        });
        return response;


    };
    this.Getdrugalldeploc = function (id,id1) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/Getdrugalldeploc",
            params: {
                id: JSON.stringify(id),
                id1: JSON.stringify(id1),

            }
        });
        return response;


    };
    this.GetdrugalldepWard = function (id) {

        var response = $http({
            method: "post",
            url: "/Ward/GetdrugalldepWard",
            params: {
                id: JSON.stringify(id)


            }
        });
        return response;


    };

    this.Getclaim = function (id,id1) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getclaim",
            params: {
                id: JSON.stringify(id),
                id1: JSON.stringify(id1)

            }
        });
        return response;


    };
    this.Getgvsclaim = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getgvsclaim",
            params: {
                id: JSON.stringify(id)
               

            }
        });
        return response;


    };
    this.Getgvsclaim1 = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getgvsclaim1",
            params: {
                id: JSON.stringify(id)


            }
        });
        return response;


    };
    this.Getgvsclaim2 = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getgvsclaim2",
            params: {
                id: JSON.stringify(id)


            }
        });
        return response;


    };
      this.Getretiredclaim = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getretiredclaim",
            params: {
                id: JSON.stringify(id),


            }
        });
        return response;


      };
      this.Getremclaim = function (id,id1) {

          var response = $http({
              method: "post",
              url: "/claim_detail/Getremclaim",
              params: {
                  id: JSON.stringify(id),
                  id1: JSON.stringify(id1),

              }
          });
          return response;


      };
    this.Getretclaim = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getretclaim",
            params: {
                id: JSON.stringify(id),


            }
        });
        return response;


    };
     this.Gettsiclaim = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Gettsiclaim",
            params: {
                id: JSON.stringify(id),
               

            }
        });
        return response;


     };
     this.getuserperlist = function (id) {
         debugger;
         var response = $http({
             method: "post",
             url: "/UserPermissions/getuserperlist",
             params: {
                 id: JSON.stringify(id),


             }
         });
         return response;


     };
     this.setdhsrec = function (id) {

         var response = $http({
             method: "post",
             url: "/claim_detail/setdhsrec",
             params: {
                 id: JSON.stringify(id),


             }
         });
         return response;


     };

     this.remdiv = function (id) {

         var response = $http({
             method: "post",
             url: "/UserPermissions/remdiv",
             params: {
                 id: JSON.stringify(id),


             }
         });
         return response;


     };

this.Getdhsclaim = function (id) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Getdhsclaim",
            params: {
                id: JSON.stringify(id),
               

            }
        });
        return response;


    };
    this.savepdrug = function (id, epdr2) {

        var response = $http({
            method: "post",
            url: "/DrugItems1/savepdrug",
            params: {
                id: JSON.stringify(id),
                epdr2: JSON.stringify(epdr2)

            }
        });
        return response;


    };
    this.Getrpcdrug = function () {
        return $http.get("/DrugItems1/Getrpcdrug");
    };
    this.Getmmenu = function () {
        return $http.get("/Users/Getmmenu");
    };
    this.Getsnp = function () {
        return $http.get("/SurgeryMasters/Getsnp");
    };
    this.Getsta = function () {
        return $http.get("/SurgeryMasters/Getsta");
    };
    this.Getsut = function () {
        return $http.get("/SurgeryMasters/Getsut");
    };
    this.Getnut = function () {
        return $http.get("/SurgeryMasters/Getnut");
    };
    this.Getperdose = function () {
        return $http.get("/Patient_Detail/Getperdose");
    };
    this.Getsurmo = function () {
        return $http.get("/SurgeryMasters/Getsurmo");
    };
    this.Getsurasi = function () {
        return $http.get("/SurgeryMasters/Getsurasi");
    };
    this.Getatsur = function () {
        return $http.get("/SurgeryMasters/Getatsur");
    };
     this.Getcltyp = function () {
        return $http.get("/Users/Getcltype");
    };
     this.Getloc = function () {
         return $http.get("/Users/Getloc");
     };
     this.Getphloc = function () {
         return $http.get("/Users/Getphloc");
     };
    this.Getsurfeq = function () {
        return $http.get("/SurgeryMasters/Getsurfeq");
    };
    this.Getsurpdur = function () {
        return $http.get("/SurgeryMasters/Getsurpdur");
    };
    this.Getsurtecniq = function () {
        return $http.get("/SurgeryMasters/Getsurtecniq");
    };
    this.Getsurpom = function () {
        return $http.get("/SurgeryMasters/Getsurpom");
    };
    this.Getdrugtrans = function () {
        return $http.get("/DrugItems1/Getdrugtrans");
    };
    this.loaddrugdept = function () {
        return $http.get("/DrugItems1/loaddrugdept");
    };
    this.Getepdrug = function () {
        return $http.get("/DrugItems1/Getepdrug");
    };
    this.viewsick1 = function (id) {
     
        var response = $http({
            method: "post",
            url: "/Patient_Detail/viewsick1",
            params: {
                id: JSON.stringify(id)
               
            }
        });
        return response;


    };
    this.saveditem = function (itemdescription, qnty, dordno) {

        var response = $http({
            method: "post",
            url: "/DrugItems/saveditem",
            data: $.param({
                itemdescription: JSON.stringify(itemdescription),
                dordno: JSON.stringify(dordno),
                qnty: JSON.stringify(qnty)

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    //Get POR
    this.getPOR = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPOR",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };
    //get POR details
    this.getPorDtls = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPorDtls",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    // get Employee By Id
    this.getrole = function (roleID) {
        var response = $http({
            method: "post",
            url: "/api/GetRole",
            params: {
                id: JSON.stringify(roleID)
            }
        });
        return response;
    }

    // Update Employee
    this.updateRole = function (role) {
        var response = $http({
            method: "post",
            url: "/api/PostRole",
            data: JSON.stringify(role),
            dataType: "json"
        });
        return response;
    }

    // Add Employee
    this.AddEpisode = function (por) {
        debugger;

        var response = $http({
            method: "post",
            url: "EpisodCreate",
            data: JSON.stringify(por),
            dataType: "json"
        });
        return response;
    }

    //Add POR Details
    this.AddDetails = function (porDetails) {
        debugger;

        var response = $http({
            method: "post",
            url: "PORDtlsCreate",
            data: JSON.stringify(porDetails),
            dataType: "json"
        });
        return response;
    }

    //Delete Employee
    this.DeletePorDtls = function (roleID) {
        debugger;
        var response = $http({
            method: "post",
            url: "DeletePorDtls",
            params: {
                id: JSON.stringify(roleID),
            }

        });
        return response;
    }
    this.GetHyperSubType1 = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetHyperSubType",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        
    };
    this.GetReactSubType = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetReactSubType",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;


    };
    this.Getsurgpatient = function (id) {

        var response = $http({
            method: "post",
            url: "/SurgeryMasters/Getsurgatient",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;


    };
    this.PatientHystoryfp = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystoryfp",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.GetOPDPatient = function (id) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetOPDPatient",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;


    };

    this.GetOPDPatientWard = function (id) {

        var response = $http({
            method: "post",
            url: "/Ward/GetOPDPatientWard",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;


    };
    
    this.Submitgenen = function (genent, PID) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/Submitgenen",
            data: $.param({
               
                genent: JSON.stringify(genent),
                PID: JSON.stringify(PID)
               
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.Submitpatient = function (SubCategoryID,CategoryID, items, hitems, Present_Complain, PID, HDetail) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/Submitpatient",
            data:$.param({
                items: JSON.stringify(items),
                hitems: JSON.stringify(hitems),
                Present_Complain: JSON.stringify(Present_Complain),
                PID: JSON.stringify(PID),
                SubCategoryID: JSON.stringify(SubCategoryID),
                CategoryID: JSON.stringify(CategoryID),
                HDetail: JSON.stringify(HDetail)
            }),headers:{'Content-Type':'application/x-www-form-urlencoded'}
        });
        return response;


    };

    this.Submitsurgery = function (PID, doa, dos, dod, tt, nop, toa, ind, suitems, attsrg, aby, ant, moa, sst, set, Catheter, Ivline, Epidural, find, prced, drins, matItems, pomitems, moins, nutr, nutri, ditems, spins,nurs) {

        var response = $http({
            method: "post",
            url: "/SurgeryMasters/Submitsurgery",
            data: $.param({
            PID: JSON.stringify(PID),
            doa: JSON.stringify(doa),
            dos: JSON.stringify(dos),
            dod: JSON.stringify(dod),
            tt: JSON.stringify(tt),
            nop: JSON.stringify(nop),
            toa: JSON.stringify(toa),
            ind: JSON.stringify(ind),
            suitems: JSON.stringify(suitems),
            attsrg: JSON.stringify(attsrg),
            aby: JSON.stringify(aby),
            ant: JSON.stringify(ant),
            moa: JSON.stringify(moa),
            sst: JSON.stringify(sst),
            set: JSON.stringify(set),
            Catheter: JSON.stringify(Catheter),
            Ivline: JSON.stringify(Ivline),
            Epidural: JSON.stringify(Epidural),
            find: JSON.stringify(find),
            prced: JSON.stringify(prced),
            drins: JSON.stringify(drins),
            matItems: JSON.stringify(matItems),
            pomitems: JSON.stringify(pomitems),
            moins: JSON.stringify(moins),
            nutr: JSON.stringify(nutr),
            nutri: JSON.stringify(nutri),
            ditems: JSON.stringify(ditems),
            spins: JSON.stringify(spins),
                nurs: JSON.stringify(nurs)
               
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Submitdischarge = function (pdid, doa, dod, dgn1, pcomp, hpcomp, pastmed, pastsurg, allgi, mgh, suitems, disind, fupins, bhtNo, counsult, cat, invSummery) {

        var response = $http({
            method: "post",
            url: "/SurgeryMasters/Submitdischarge",
            data: $.param({
                pdid: JSON.stringify(pdid),
                doa: JSON.stringify(doa),
                dod: JSON.stringify(dod),
                dgn1: JSON.stringify(dgn1),
                pcomp: JSON.stringify(pcomp),
                hpcomp: JSON.stringify(hpcomp),
                pastmed: JSON.stringify(pastmed),
                pastsurg: JSON.stringify(pastsurg),
                allgi: JSON.stringify(allgi),
                mgh: JSON.stringify(mgh),
                suitems: JSON.stringify(suitems),
                disind: JSON.stringify(disind),
                bhtNo: JSON.stringify(bhtNo),
                counsult: JSON.stringify(counsult),
                cat: JSON.stringify(cat),
                fupins: JSON.stringify(fupins),
                invSummery: JSON.stringify(invSummery),              
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    ////
    this.Submitsicknurs = function (PID, livein, age, tservice, forduty, defaulter,loc,div) {

        var response = $http({
            method: "post",
            url: "/SickReports/Submitsicknurs",
            data: $.param({
                PID: JSON.stringify(PID),
                livein: JSON.stringify(livein),
                age: JSON.stringify(age),
                tservice: JSON.stringify(tservice),
                forduty: JSON.stringify(forduty),
                defaulter: JSON.stringify(defaulter),
                loc: JSON.stringify(loc),
                div: JSON.stringify(div)
           


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Submitsick = function (PID, livein, age, tservice, forduty, defaulter, tmedexam,formed) {

        var response = $http({
            method: "post",
            url: "/SickReports/Submitsick",
            data: $.param({
                PID: JSON.stringify(PID),
                livein: JSON.stringify(livein),
                age: JSON.stringify(age),
                tservice: JSON.stringify(tservice),
                forduty: JSON.stringify(forduty),
                defaulter: JSON.stringify(defaulter),
                formed: JSON.stringify(formed),
                tmedexam: JSON.stringify(tmedexam)


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Submitmedex2 = function (PID, bp, medmes, fit, msid, resn, tcol, fbs) {

        var response = $http({
            method: "post",
            url: "/SickReports/Submitmedex2",
            data: $.param({
                PID: JSON.stringify(PID),
               
                bp: JSON.stringify(bp),
                medmes: JSON.stringify(medmes),
                msid: JSON.stringify(msid),
                fit: JSON.stringify(fit),
                resn: JSON.stringify(resn),
                tcol: JSON.stringify(tcol),
                fbs: JSON.stringify(fbs)
                

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };



    this.Submitmedex = function (PID, age,hght,wght,bmi, bp, vsion,hbac,usugar,fbs, tcol,  execg,tri,ldl,yrr,vsion2,sess1) {
        debugger;
        var response = $http({
         
            method: "post",
            url: "/SickReports/Submitmedex",
            data: $.param({
             
                PID: JSON.stringify(PID),
                hght: JSON.stringify(hght),
                age: JSON.stringify(age),
                wght: JSON.stringify(wght),
                bmi: JSON.stringify(bmi),
                bp: JSON.stringify(bp),
                    vsion: JSON.stringify(vsion),
            hbac: JSON.stringify(hbac),
            usugar: JSON.stringify(usugar),
            fbs :JSON.stringify(fbs),
            tcol: JSON.stringify(tcol),
            execg: JSON.stringify(execg),
            tri: JSON.stringify(tri),
            ldl: JSON.stringify(ldl),
            yrr: JSON.stringify(yrr),
            sess1: JSON.stringify(sess1),
            vsion2: JSON.stringify(vsion2)
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Saveuser = function (ServiceNo, fname, lname, pass, LocationID, DivisionID,ClinicTypeID,userItems) {

        var response = $http({
            method: "post",
            url: "/Users/Saveuser",
            data: $.param({
                ServiceNo: JSON.stringify(ServiceNo),
                fname: JSON.stringify(fname),
                lname: JSON.stringify(lname),
                pass: JSON.stringify(pass),
                LocationID: JSON.stringify(LocationID),
                ClinicTypeID: JSON.stringify(ClinicTypeID),
                DivisionID: JSON.stringify(DivisionID),
                userItems: JSON.stringify(userItems)


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Savemenu = function (ServiceNo,LocationID, DivisionID, ClinicTypeID, menuid) {

        var response = $http({
            method: "post",
            url: "/Users/Savemenu",
            data: $.param({
                ServiceNo: JSON.stringify(ServiceNo),
                LocationID: JSON.stringify(LocationID),
                ClinicTypeID: JSON.stringify(ClinicTypeID),
                DivisionID: JSON.stringify(DivisionID),
                menuid: JSON.stringify(menuid)


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    ///
    this.Submitlabpatient = function (SubCategoryID, CategoryID, litems, Present_Complain, PID) {

        var response = $http({
            method: "post",
            url: "/Patient_Detail/Submitlabpatient",
            data: $.param({
              
                Present_Complain: JSON.stringify(Present_Complain),
                litems: JSON.stringify(litems),
                SubCategoryID: JSON.stringify(SubCategoryID),
                CategoryID: JSON.stringify(CategoryID),
                PID: JSON.stringify(PID)
               
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    ////
    this.Submitclaim = function (clitems, PID) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Submitclaim",
            data: $.param({

                clitems: JSON.stringify(clitems),
                PID: JSON.stringify(PID)

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Submittsi = function (cliitems) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Submittsi",
            data: $.param({
                cliitems: JSON.stringify(cliitems),
              

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.Submitgvs = function (cliitems) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Submitgvs",
            data: $.param({
                cliitems: JSON.stringify(cliitems),
              

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.Submitclup = function (cliitems) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Submitclup",
            data: $.param({
                cliitems: JSON.stringify(cliitems),


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Submitdhs = function (cliitems) {

        var response = $http({
            method: "post",
            url: "/claim_detail/Submitdhs",
            data: $.param({
                cliitems: JSON.stringify(cliitems),
               

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    //////
    this.Savesms = function (pdd, smstext, pntext) {

        var response = $http({
            method: "post",
            url: "/Lab_sms/Savesms",
            data: $.param({
                pdd: JSON.stringify(pdd),
                smstext: JSON.stringify(smstext),
                pntext: JSON.stringify(pntext)
               
            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    //////
    this.getbalance = function (catid, svc) {
        debugger;
        var response = $http({
            method: "post",
            url: "/claim_detail/getbalance",
            data: $.param({
                catid: JSON.stringify(catid),
                svc: JSON.stringify(svc)
              

            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };


    this.getbalancetsi = function ( svc) {
        debugger;
        var response = $http({
            method: "post",
            url: "/claim_detail/getbalancetsi",
            data: $.param({
               
                svc: JSON.stringify(svc)


            }), headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };

    this.Savepatient = function (bitems,reftohosp, planofmgt, drughyst, hitems, pastmedhys, items, sitems, litems, ditems, Present_Complain, History_PresentComplain, Other_Complain, History_OtherComplain, OPD_Diagnosis,  AbdOther, PDID, pntstatus, GClinic, dgn1,genex, cardex, cenex, resex, othex, abdex,drugins) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/Savepatient",
            data: $.param({
                reftohosp: JSON.stringify(reftohosp),
                planofmgt: JSON.stringify(planofmgt),
                drughyst: JSON.stringify(drughyst),
                hitems: JSON.stringify(hitems),
                pastmedhys: JSON.stringify(pastmedhys),
                items: JSON.stringify(items),
                sitems: JSON.stringify(sitems),
                litems: JSON.stringify(litems),
                ditems: JSON.stringify(ditems),
                bitems: JSON.stringify(bitems),
                Present_Complain:JSON.stringify(Present_Complain),
                History_PresentComplain:JSON.stringify(History_PresentComplain),
                Other_Complain: JSON.stringify(Other_Complain),
                History_OtherComplain: JSON.stringify(History_OtherComplain),
                OPD_Diagnosis: JSON.stringify(OPD_Diagnosis),
                AbdOther: JSON.stringify(AbdOther),
                dgn1:JSON.stringify(dgn1),
                PDID: JSON.stringify(PDID),
                genex: JSON.stringify(genex),
                cardex: JSON.stringify(cardex),
                cenex: JSON.stringify(cenex),
                resex: JSON.stringify(resex),
                othex: JSON.stringify(othex),
                abdex: JSON.stringify(abdex),
                drugins: JSON.stringify(drugins),
                pntstatus: JSON.stringify(pntstatus),
                GClinic: JSON.stringify(GClinic)
           }),
           headers:{'Content-Type':'application/x-www-form-urlencoded'}
        });
        return response;


    };
    this.Savedemand = function (itemno, qnty) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Drug_Prescription/Savedemand",
            data: $.param({
                itemno: JSON.stringify(itemno),
                qnty: JSON.stringify(qnty)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.Savestck = function (itemno, qnty) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Drug_Prescription/Savestck",
            data: $.param({
                itemno: JSON.stringify(itemno),
                qnty: JSON.stringify(qnty)
               
            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        });
        return response;


    };
    this.SavepatientWard = function (suitems, bitems, reftohosp, planofmgt, drughyst, hitems, pastmedhys, items, sitems, litems, ditems, Present_Complain, History_PresentComplain, Other_Complain, History_OtherComplain, AbdOther, PDID, pntStatus, GClinic, dgn1, genex, cardex, cenex, resex, othex, abdex, drugins, remarks, wardNo, bedNo, mgtPlan, OPD_Diagnosis, PatientCom) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Ward/SavepatientWard",
            data: $.param({
                reftohosp: JSON.stringify(reftohosp),
                planofmgt: JSON.stringify(planofmgt),
                drughyst: JSON.stringify(drughyst),
                hitems: JSON.stringify(hitems),
                pastmedhys: JSON.stringify(pastmedhys),
                items: JSON.stringify(items),
                sitems: JSON.stringify(sitems),
                litems: JSON.stringify(litems),
                ditems: JSON.stringify(ditems),
                bitems: JSON.stringify(bitems),
                Present_Complain:JSON.stringify(Present_Complain),
                History_PresentComplain:JSON.stringify(History_PresentComplain),
                Other_Complain: JSON.stringify(Other_Complain),
                History_OtherComplain: JSON.stringify(History_OtherComplain),
                AbdOther: JSON.stringify(AbdOther),
                dgn1:JSON.stringify(dgn1),
                PDID: JSON.stringify(PDID),
                genex: JSON.stringify(genex),
                cardex: JSON.stringify(cardex),
                cenex: JSON.stringify(cenex),
                resex: JSON.stringify(resex),
                othex: JSON.stringify(othex),
                abdex: JSON.stringify(abdex),
                drugins: JSON.stringify(drugins),
                pntStatus: JSON.stringify(pntStatus),
                GClinic: JSON.stringify(GClinic),
                remarks: JSON.stringify(remarks),
                wardNo: JSON.stringify(wardNo),
                bedNo: JSON.stringify(bedNo),
                OPD_Diagnosis: JSON.stringify(OPD_Diagnosis),
                mgtPlan: JSON.stringify(mgtPlan),
                PatientCom: JSON.stringify(PatientCom),
                suitems: JSON.stringify(suitems),
           }),
           headers:{'Content-Type':'application/x-www-form-urlencoded'}
        });
        return response;


    };
    
    //Get Locations
    this.GetVitalTypes = function () {
        return $http.get("/Patient_Detail/GetVitalTypes");
    };
    this.GetpatiantsubType = function () {
        return $http.get("/Patient_Detail/GetpatiantsubType");
    };
    this.GetpatiantType = function () {
        return $http.get("/Patient_Detail/GetpatiantType");
    };

    this.GetWardTypes = function () {
        return $http.get("/Ward/GetWardTypes");
    };

    this.GetSeverityTypes = function () {
        return $http.get("/Patient_Detail/GetSeverityTypes");
    };
    this.GetMethod = function () {
        return $http.get("/Patient_Detail/GetMethod");
    };
    this.Getmes= function () {
        return $http.get("/Patient_Detail/Getmes");
    };
    this.GetRoute = function () {
        return $http.get("/Patient_Detail/GetRoute");
    };
    this.GetSt = function () {
        return $http.get("/Patient_Detail/GetSt");
    };

    this.GetStatusWard = function () {
        return $http.get("/Ward/GetStatusWard");
    };

    this.Getdgn = function () {
        return $http.get("/Patient_Detail/Getdgn");
    };
    this.Getclmcat = function () {
        return $http.get("/claim_detail/Getclmcat");
    };
    this.GetClinics = function () {
        return $http.get("/Patient_Detail/GetClinics");
    };
    this.GetDrug = function () {
        return $http.get("/Patient_Detail/GetDrug");
    };
    this.GetDrugWard = function () {
        return $http.get("/Ward/GetDrugWard");
    };
    this.GetDrugtime = function () {
        return $http.get("/Patient_Detail/GetDrugtime");
    };
    this.GetMedkw = function () {
        return $http.get("/Patient_Detail/GetMedkw");
    };
    this.Getlablist = function () {
        return $http.get("/Patient_Detail/Getlablist");
    };
    this.Getsmslablist = function () {
        return $http.get("/Patient_Detail/Getsmslablist");
    };
    this.GetSicktype = function () {
        return $http.get("/Patient_Detail/GetSicktype");
    };
    this.GetHyperReactType = function () {
        return $http.get("/Patient_Detail/GetHyperReactType");
    };
    this.GetHyperMainType = function () {
        return $http.get("/Patient_Detail/GetHyperTypes");
    };
    //Get Relationship types
    this.GetRelationships = function () {
        debugger;
        return $http.get("/Patient_Detail/GetRelationships");
    };

    //Get user location
    this.getUserLocation = function () {
        return $http.get("/POR_Header/GetUserLocation");
    };

    //Get Main Category
    this.getMainCategorys = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetMainCategory",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.PatientHystory = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystorynewb",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.PatientHystorypft = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystorypft",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.PatientHystoryclaim = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystoryclaim",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.PatientHystorygvs = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystorygvs",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.PatientHystory1 = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/PatientHystory1",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.PatientHystoryWard = function (id, id2) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Ward/PatientHystoryWard",
            params: {
                id: JSON.stringify(id),
                id2: JSON.stringify(id2)
            }
        });
        return response;

    };

    this.PatientHystoryWardDrugs = function (id, id2) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Ward/PatientHystoryWardDrugs",
            params: {
                id: JSON.stringify(id),
                id2: JSON.stringify(id2)
            }
        });
        return response;

    };

    //Get Sub Category
    this.getSubCategorys = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetSubCategory",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get gs division
    this.getDSDivision = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetDSDivision",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Town
    this.getTown = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetTown",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };

    //Get Police Station
    this.getPoliceStation = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetPoliceStation",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;
    };
    this.GetPatientsick3 = function (id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientsick3",
            data: $.param({
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.GetPatients = function (stp,id,relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatients",
            data: $.param({
                stp: JSON.stringify(stp),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            } ),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
            
        });
        return response;

    };
    this.GetPatientpurch = function (stp, id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientpurch",
            data: $.param({
                stp: JSON.stringify(stp),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.GetPatientlab = function (stp, id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientslab",
            data: $.param({
                stp: JSON.stringify(stp),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.GetPatientmedical = function (st,id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientmedical",
            data: $.param({
                st: JSON.stringify(st),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };

    this.viewmsts = function (id, relet,sess1) {
        debugger;
        var response = $http({
            method: "post",
            url: "/SickReports/getmsts",
            data: $.param({
                id: JSON.stringify(id),
                sess1: JSON.stringify(sess1),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };


    this.GetPatientsall = function (st,id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientsall",
            data: $.param({
                st: JSON.stringify(st),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.Getuser = function (id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/Getuser",
            data: $.param({
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.GetPatientsick = function (st,id, relet) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetPatientsick",
            data: $.param({
                st: JSON.stringify(st),
                id: JSON.stringify(id),
                relet: JSON.stringify(relet)

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };
    this.Getuserdept = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Users/Getuserdept",
            data: $.param({
                id: JSON.stringify(id)
                

            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };

    this.loadsubloc = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Users/loadsubloc",
            data: $.param({
                id: JSON.stringify(id)


            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };

    this.loginuser = function (username,passwd,Clinic_ID,DivisionID) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Users/loginuser",
            data: $.param({
                username: JSON.stringify(username),
                passwd: JSON.stringify(passwd),
                Clinic_ID: JSON.stringify(Clinic_ID),
                DivisionID: JSON.stringify(DivisionID)


            }),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }

        });
        return response;

    };



    this.submitvital = function (ite) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/submitvitals",
            params: {
                id: JSON.stringify(ite)

            }
        });
        return response;

    };

    //this.loadchildimg = function (id) {
    //    debugger;
    //    var response = $http({
    //        method: "post",
    //        url: "/Patient_Detail/loadchildimg",
    //        params: {
    //            id: JSON.stringify(id)

    //        }
    //    });
    //    return response;

    //};
    this.loadchildimg1 = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/loadchildimg1",
            params: {
                id: JSON.stringify(id)

            }
        });
        return response;

    };
    this.loadimgbystp = function (stp,id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/loadimgbystp",
            params: {
                stp: JSON.stringify(stp),
                id: JSON.stringify(id)

            }
        });
        return response;

    };
    this.Getservicenoedt = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/Getservicenoedt",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    this.getsp = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadServicePerson",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    }; 
    this.Setlocids = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Users/Setlocids",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.getrnks = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadRanks",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.getrelation = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadRelationtype",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    this.getsex = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patients/LoadSex",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };
    //Get Sub Category
    this.getFormation = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetFormation",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Service Personnel Details
    this.getServicePersonnel = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/Patient_Detail/GetServicePersonnel",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Promotion authority Details
    this.getAuthRef = function (authority, Subcat) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetAuthRef",
            params: {
                id: JSON.stringify(authority),
                Subcat: JSON.stringify(Subcat)
            }
        });
        return response;

    };

    //Get Ranks
    this.getRanks = function () {
        debugger;
        return $http.get("/POR_Header/GetRank");
    };

    //Get Trades
    this.getTrade = function () {
        debugger;
        return $http.get("/POR_Header/GetTrade");
    };

    //Get Districts
    this.getDistricts = function () {
        debugger;
        return $http.get("/POR_Header/GetDistricts");
    };

    //Get nature of occurances
    this.getNatureofOccurance = function (id) {

        var response = $http({
            method: "post",
            url: "/POR_Header/GetNatureofOccurance",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

        //id = "AA";
        //alert(id);
        //return $http.get("/POR_Header/GetSubCategory/" +id);
    };

    //Get Max Occurance NO
    this.getMaxOccuranceNo = function (id) {
        debugger;
        var response = $http({
            method: "post",
            url: "/POR_Header/GetMaxOccuranceNo",
            params: {
                id: JSON.stringify(id)
            }
        });
        return response;

    };

    // get Employee By Id
    this.getrole = function (roleID) {
        var response = $http({
            method: "post",
            url: "/api/GetRole",
            params: {
                id: JSON.stringify(roleID)
            }
        });
        return response;
    }

});