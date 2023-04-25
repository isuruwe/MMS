
//angular.module('EpisodeCntrl', [ 'ui.bootstrap']);

app.controller("EpisodeCntrl", function ($scope, EpisodeService) {
    $scope.divPor = true;

    $scope.startsWith = function(itemdescription, viewValue) {
        return itemdescription.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
} 
   
    $scope.Year = new Date().getFullYear();
    $scope.porType = 1;
    $scope.WefDate = new Date;
    $scope.entryType = 1;
    $scope.subButton = "";

    $scope.pidnw = "";
    $scope.st = 2;
    $scope.name = "asd";
    $scope.amount;
    $scope.vitals = "";
    $scope.items = [];
    $scope.initsick = function () {
        debugger;
        GetVitalType();
        GetpatiantsubType();
        GetpatiantType();
        GetRelationship();
        GetHyperMainType();
        GetHyperReactType();
        GetSeverityTypes();
        GetMedkw();
        Getloc();
    }
    $scope.initdrug = function () {
        debugger;
        $scope.DName = JSON.parse(localStorage.getItem("dlist1"));
       
        if ($scope.DName == '' || $scope.DName == undefined) {
          Getperdose();
        GetDrug();
        GetDrugtime();
        } else {
            Getperdose();
            $scope.states = $scope.DName.itemdescription;
            GetDrugtime();
        }
      
    }
    $scope.initlab = function () {
        debugger;
        GetVitalType();
        GetRelationship();
        Getlablist();
        GetpatiantsubType();
        GetpatiantType();
    }
    $scope.inituser = function () {
        debugger;
        Getloc();
        Getcltyp();
        Getmmenu();
    }
    $scope.initloc = function () {
        Getloc();
    }

     $scope.psindex1Ward = "";
     $scope.issudqnty1Ward = "";
    $scope.psitemsWard = [];
    $scope.adddrugItemWard = function (PlanId, nurseRemarks) {
        debugger;
        for (var i = 0; i < $scope.psitemsWard.length; i++) {
            if ($scope.psitemsWard[i].psindex1Ward == PlanId) {
                $scope.psitemsWard.splice(i, 1);
            }
        }
        $scope.psitemsWard.push({
           
            psindex1Ward: PlanId,
            issudqnty1Ward: nurseRemarks

        });
       
        $scope.Ps_Index = "";
        $scope.issudqnty = "";
    }

    $scope.initphloc = function () {
        Getphloc();
        $scope.DName = JSON.parse(localStorage.getItem("dlist1"));
        if ($scope.DName == '' || $scope.DName == undefined) {
            Getperdose();
            GetDrug();
            GetDrugtime();
        } else {
            Getperdose();
            $scope.states = $scope.DName.itemdescription;
            GetDrugtime();
        }
    }
    $scope.initmed = function () {
        Getmes();
    }
  
    $scope.initsur = function () {
        debugger;
        $scope.DName = JSON.parse(localStorage.getItem("dlist1"));

        if ($scope.DName == '' || $scope.DName == undefined) {
            GetDrug();
        }
        Getatsur();
        Getdgn();
        GetVitalType();
        Getsnp();
        Getnut();
        GetRoute();
        GetMethod();
        Getsut();
        GetDrugtime();
        Getperdose();
        GetpatiantsubType();
        GetpatiantType();
        GetRelationship();
        Getsta();
        Getsurmo();
        Getsurasi();
        Getsurfeq();
        Getsurpdur();
        Getsurtecniq();
        Getsurpom();
        $scope.attsrg = "Dr. Keshara C. Ratnatunga (VS)";
        $scope.ant = "Dr. PS Amarasekara (CA)";
    }
    
    $scope.initpatient = function () {
        debugger;
        $scope.DName = JSON.parse(localStorage.getItem("dlist1"));
       
        if ($scope.DName == '' || $scope.DName == undefined) {
            GetDrug();
        }

        var date = new Date();
        
        $scope.Sickeff = date;

            //date.toLocaleFormat('%y/%b/%d');
            //toISOString().substr(0, 10);
        GetHyperMainType();
        GetVitalType();
        GetRoute();
        GetMethod();
       
        GetDrugtime();
        Getperdose();
        Getlablist();
        Getsmslablist();
        GetSicktype();
        GetSt();
        Getdgn();
        GetClinics();
        GetWardTypes();
    }

    $scope.initpatientWard = function () {
        debugger;
        $scope.DName = JSON.parse(localStorage.getItem("dlistWard"));

        if ($scope.DName == '' || $scope.DName == undefined) {
            //GetDrug();
            GetDrugWard();
        }

        var date = new Date();

        $scope.Sickeff = date;

        //date.toLocaleFormat('%y/%b/%d');
        //toISOString().substr(0, 10);
        GetHyperMainType();
        GetVitalType();
        GetRoute();
        GetMethod();

        GetDrugtime();
        Getperdose();
        Getlablist();
        Getsmslablist();
        GetSicktype();
        GetStatusWard();
        Getdgn();
        GetClinics();
        GetWardTypes();
    }

    $scope.initclaim = function () {
        debugger;
        var date = new Date();

        $scope.evntdt = date;
        GetRelationship();
        Getclmcat();

        GetpatiantsubType();
        GetpatiantType();
        
        Getdgn();
       
    }
    //////////////////////////
    $scope.Savedo = function () {
        debugger;
        var getData = EpisodeService.Savedo($scope.itemsdo);

        $scope.showerror = false; getData.then(function (pderror) {
            window.location.reload();
            $scope.submiterror = pderror.data;
        }, function () {
            $scope.showerror = true;
        });

    }



    /////////////////////////
    $scope.LOC1 = "";
    $scope.isqty1 = "";
    $scope.rtnqty1 = "";
    $scope.rmks1 = "";
    $scope.batchid1 = "";
    $scope.itemsdo = [];
    $scope.addItemdo = function (LOC, batchid, isqty, rtnqty, rmks) {
        debugger;


        if (LOC) {

          
            for (var i = 0; i < $scope.itemsdo.length; i++) {
                if ($scope.itemsdo[i].LOC1 == LOC) {
                    $scope.itemsdo.splice(i, 1);
                }
            }


            //   if (!exist) {
            $scope.itemsdo.push({
                LOC1: LOC,
                batchid1: batchid,
                isqty1: isqty,
                rtnqty1: rtnqty,
                rmks1: rmks
               
            });
            // }
        }
        $scope.LOC = "";
        $scope.isqty = "";
        $scope.rtnqty = "";
        $scope.rmks = "";
        $scope.batchid = "";
    }

    $scope.dohremoveItem = function (index) {
        debugger;
        $scope.itemsdo.splice(index, 1);
    }




    ////////////////////////




    $scope.saverpcdrug = function (id) {

        $scope.epasdr
        var getData = EpisodeService.saverpcdrug(id, $scope.rpcdr2);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.epasdr1 = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.savepdrug = function (id) {

        $scope.epasdr
        var getData = EpisodeService.savepdrug(id, $scope.epdr2);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.epasdr2 = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     $scope.savetrnsdrug = function (id) {

       
        var getData = EpisodeService.savetrnsdrug(id, $scope.drugtrqt, $scope.deptid.Clinic_ID);
      
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.drugtr2 = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.initdrug1 = function () {
        debugger;
        Getdrugtrans();
        Getrpcdrug();
        Getepdrug();
        loaddrugdept();
    }
    function Getrpcdrug() {
        debugger;
        var getData = EpisodeService.Getrpcdrug();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.rpcdr = mod.data;
            $scope.states = $scope.rpcdr.ItemDescription;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function Getepdrug() {
        debugger;
        var getData = EpisodeService.Getepdrug();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.epdr = mod.data;
            $scope.states = $scope.epdr.itemdescription;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function loaddrugdept() {
        debugger;
        var getData = EpisodeService.loaddrugdept();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.drdept = mod.data;
           
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getdrugtrans() {
        debugger;
        var getData = EpisodeService.Getdrugtrans();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.drugtr = mod.data;
            $scope.states = $scope.drugtr.itemdescription;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Getdrugonr = function (id) {
        $scope.showloader = true;
        $scope.epasonr
        var getData = EpisodeService.Getdrugonr(id);
        $scope.epasonr = null;
        epasdr1 = "";
        epasdr2 = "";
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasonr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getdrugalldep = function (id) {
        $scope.showloader = true;
        $scope.epasonr
        var getData = EpisodeService.Getdrugalldep(id);
        $scope.epasonr = null;
        epasdr1 = "";
        epasdr2 = "";
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasonr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getdrugalldeploc = function (id, id1) {
        debugger;
        $scope.showloader = true;
        $scope.epasonr
        var getData = EpisodeService.Getdrugalldeploc(id,id1);
        $scope.epasonr = null;
        epasdr1 = "";
        epasdr2 = "";
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasonr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.GetdrugalldepWard = function (id) {
        debugger;
        $scope.showloader = true;
        $scope.epasonr
        var getData = EpisodeService.GetdrugalldepWard(id);
        $scope.epasonr = null;
        epasdr1 = "";
        epasdr2 = "";
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasonr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.setdhsrec = function (id) {
        $scope.showloader = true;
       
        var getData = EpisodeService.setdhsrec(id);
       

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
           

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.remdiv = function (id) {
        $scope.showloader = true;

        var getData = EpisodeService.remdiv(id);


        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.getuserperlist();

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    $scope.Getdhsclaim = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.Getdhsclaim($scope.regno);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.clmdx = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Gettsiclaim = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.Gettsiclaim($scope.regno);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.clmdx = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.getuserperlist = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.getuserperlist($scope.ServiceNo);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.stmdx = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    $scope.Getretiredclaim = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.Getretiredclaim($scope.regno3);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.retstat = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Getremclaim = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.Getremclaim($scope.regno4, $scope.regno5);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.clremdx = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     $scope.Getretclaim = function () {
        $scope.showloader = true;
        $scope.clmdx
        var getData = EpisodeService.Getretclaim($scope.regno2);
        $scope.clmdx = null;

        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.clrdx = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
       $scope.Getclaim = function () {
        $scope.showloader = true;
        $scope.clmd
        var getData = EpisodeService.Getclaim($scope.ServiceNo, $scope.evntdt);
        $scope.clmd = null;
        
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.clmd = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

       }
       $scope.Getgvsclaim1 = function () {
           $scope.showloader = true;
           $scope.clmd
           var getData = EpisodeService.Getgvsclaim1($scope.regno);
           $scope.clmd = null;

           $scope.showerror = false; getData.then(function (sick) {
               debugger;
               $scope.showloader = false;
               $scope.gvscl = sick.data;

           }, function () {
               $scope.showloader = false; $scope.showerror = true;
           });

       }
       $scope.Getgvsclaim2 = function () {
           $scope.showloader = true;
           $scope.clmd
           var getData = EpisodeService.Getgvsclaim2($scope.svc);
           $scope.clmd = null;

           $scope.showerror = false; getData.then(function (sick) {
               debugger;
               $scope.showloader = false;
               $scope.gvscl2 = sick.data;

           }, function () {
               $scope.showloader = false; $scope.showerror = true;
           });

       }

       $scope.Getgvsclaim = function () {
           $scope.showloader = true;
           $scope.clmd
           var getData = EpisodeService.Getgvsclaim($scope.ServiceNo);
           $scope.clmd = null;

           $scope.showerror = false; getData.then(function (sick) {
               debugger;
               $scope.showloader = false;
               $scope.gvscl = sick.data;

           }, function () {
               $scope.showloader = false; $scope.showerror = true;
           });

       }
    $scope.addItem = function (vital, vvalue) {
        debugger;

        if (vital == "1") {
            vitals = "Temperature"
        }
        if (vital == "2") {
            vitals = "Pulse"
        }
        if (vital == "3") {
            vitals = "Blood Pressure"
        }
        if (vital == "4") {
            vitals = "Respiratory Rate"
        }
        if (vital == "5") {
            vitals = "Blood Oxygen Saturation"
        }
        if (vital == "6") {
            vitals = "Height"
        }
        if (vital == "7") {
            vitals = "Weight"
        }

        $scope.items.push({
            name: vitals,
            vid: vital,
            amount: vvalue

        });
        $scope.vital = "";
        $scope.vvalue = "";
    }

    $scope.removeItem = function (index) {
        debugger;
        $scope.items.splice(index, 1);
    }
    $scope.htype = "";
    $scope.htype1 = "";
    $scope.hsubtype = "";
    $scope.hsubtype1 = "";
    $scope.hrtype = "";
    $scope.hrtype1 = "";
    $scope.hrsubtype = "";
    $scope.hrsubtype1 = "";
    $scope.hstype = "";
    $scope.hstype1 = "";
    $scope.hitems = [];
    $scope.haddItem = function (htype21, htype2, hstype21) {
        debugger;



        $scope.hitems.push({
            htype: htype2,
            htype1: htype21,


            hstype1: hstype21

        });
        $scope.htype2 = "";
        $scope.htype21 = "";

        $scope.hstype2 = "";
        $scope.hstype21 = "";
    }

    $scope.hremoveItem = function (index) {
        debugger;
        $scope.hitems.splice(index, 1);
    }


    $scope.dItemno = "";
    $scope.dItemno1 = "";
    $scope.dDose = "";

    $scope.dRoute = "";
    $scope.dRoute1 = "";
    $scope.dMethod = "";
    $scope.dMethod1 = "";
    $scope.dDuration = "";
    $scope.dStockTypeID = "";
    $scope.disregdr = "";
    $scope.ditems = [];
    $scope.daddItem = function (Itemno, Itemno1, Dose, Route, Route1, Method, Method1, Duration, StockTypeID,isregdr) {
        debugger;
       
        if (Duration == '' || Duration == undefined) {
            $("#errormsgspan").text("Please Add no of Days");
            //return false;
        }
        else if (Method1 == undefined) {
            $("#errormsgspan").text("Please Select Method");
            //return false;
        }
        else if (Itemno1 == undefined) {
            $("#errormsgspan").text("Please Enter Drug");
            //return false;
        }
        else {
            $("#errormsgspan").text(" ");
            // return true;

            $scope.ditems.push({
                dItemno: Itemno,
                dItemno1: Itemno1,
                dDose: Dose,

                dRoute: Route,
                dRoute1: Route1,
                dMethod: Method,
                dMethod1: Method1,
                dDuration: Duration,
                dStockTypeID: StockTypeID,
                disregdr: isregdr,
            });
            $scope.Itemno = "";
            $scope.Itemno1 = "";
           // $scope.Dose = "";
            $scope.DName1 = "";
            $scope.Route = "";
            $scope.Route1 = "";
            $scope.Method = "";
            $scope.Method1 = "";
            $scope.Duration = "";
            $scope.StockTypeID = "";
            $scope.isregdr = "";
        }
    }

    //ward module
    $scope.daddItemWard = function (Itemno, Itemno1, Dose, Route, Route1, Method, Method1, Duration, StockTypeID) {
        debugger;

        if (Method1 == undefined) {
            $("#errormsgspan").text("Please Select Method");
            //return false;
        }
        else if (Itemno1 == undefined) {
            $("#errormsgspan").text("Please Enter Drug");
            //return false;
        }
        else {
            $("#errormsgspan").text(" ");
            // return true;

            $scope.ditems.push({
                dItemno: Itemno,
                dItemno1: Itemno1,
                dDose: Dose,

                dRoute: Route,
                dRoute1: Route1,
                dMethod: Method,
                dMethod1: Method1,
                dDuration: Duration,
                dStockTypeID: StockTypeID,

            });
            $scope.Itemno = "";
            $scope.Itemno1 = "";
            // $scope.Dose = "";
            $scope.DName1 = "";
            $scope.Route = "";
            $scope.Route1 = "";
            $scope.Method = "";
            $scope.Method1 = "";
            $scope.Duration = "";
            $scope.StockTypeID = "";
        }
    }

    $scope.dremoveItem = function (index) {
        debugger;
        $scope.ditems.splice(index, 1);
    }


    ///////////////////////////////////////////
    $scope.suItemno = "";
    $scope.suItemno1 = "";
    $scope.suDose = "";

    $scope.suRoute = "";
    $scope.suRoute1 = "";
    $scope.suMethod = "";
    $scope.suMethod1 = "";
    $scope.suDuration = "";
    $scope.suStockTypeID = "";
    $scope.suitems = [];
    $scope.suaddItem = function (Itemno, Itemno1, Dose, Route, Route1, Method, Method1, Duration, StockTypeID) {

        if (Duration == '' || Duration == undefined) {
            $("#errormsgspan").text("Please Add no of Days");
            //return false;
        } else if (Method1 == undefined) {
            $("#errormsgspan").text("Please Select Method");
            //return false;
        }
        else if (Itemno1 == undefined) {
            $("#errormsgspan").text("Please Enter Drug");
            //return false;
        }
        else {
            $("#errormsgspan").text(" ");
            // return true;

            $scope.suitems.push({
                suItemno: Itemno,
                suItemno1: Itemno1,
                suDose: Dose,

                suRoute: Route,
                suRoute1: Route1,
                suMethod: Method,
                suMethod1: Method1,
                suDuration: Duration,
                suStockTypeID: StockTypeID,

            });
            $scope.Itemno = "";
            $scope.Itemno1 = "";
            // $scope.Dose = "";
            $scope.suDName1 = "";
            $scope.Route = "";
            $scope.Route1 = "";
            $scope.Method = "";
            $scope.Method1 = "";
            $scope.Duration = "";
            $scope.StockTypeID = "";
        }
    }

  

    $scope.sdremoveItem = function (index) {
        debugger;
        $scope.suitems.splice(index, 1);
    }

    //////////////////////////////////////////


    $scope.labid = "";
    $scope.labcat = "";
    $scope.tubcat = "";

    $scope.litems = [];
    $scope.laddItem = function (tlabid, tlabcat,ttubcat) {
        debugger;

        $scope.litems.push({
            labid: tlabid,
            labcat: tlabcat,
            tubcat: ttubcat,


        });
        $scope.tlabid = "";
        $scope.tlabcat = "";
        $scope.ttubcat = "";
        $scope.lName1 = "";
    }

    $scope.lremoveItem = function (index) {
        debugger;
        $scope.litems.splice(index, 1);
    }
    /////////////////////////////////////////////////////////
    $scope.tpomid = "";
    $scope.tpomdesc = "";
    $scope.tpomfid = "";
    $scope.tpomfdesc = "";
    $scope.tpomdid = "";
    $scope.tpomduration = "";
   

    $scope.pomitems = [];
    $scope.pomItem = function (pomid, pomdesc, pomfid, pomfdesc, pomdid, pomduration) {
        debugger;

        $scope.pomitems.push({
            tpomid: pomid,
            tpomdesc: pomdesc,
            tpomfid: pomfid,
            tpomfdesc: pomfdesc,
            tpomdid: pomdid,
            tpomduration: pomduration,
        });
        $scope.pomid = "";
        $scope.pomdesc = "";
        $scope.pomfid = "";
        $scope.pomfdesc = "";
        $scope.pomdid = "";
        $scope.pomduration = "";


    }

    $scope.pomremoveItem = function (index) {
        debugger;
        $scope.pomitems.splice(index, 1);
    }




    ////////////////////////////////////////////////////////////
    
    $scope.tSMID = "";
    $scope.tSuturematerials = "";
    $scope.ttid = "";
    $scope.ttdesc = "";
    $scope.tnopkt = "";
    


    $scope.matItems = [];
    $scope.matItem = function (SMID, Suturematerials, tid, tdesc, nopkt) {
        debugger;

        $scope.matItems.push({
            tSMID: SMID,
            tSuturematerials: Suturematerials,
            ttid: tid,
            ttdesc: tdesc,
            tnopkt: nopkt,
           
        });
        $scope.SMID = "";
        $scope.Suturematerials = "";
        $scope.tid = "";
        $scope.tdesc = "";
        $scope.nopkt = "";


    }

    $scope.matremoveItem = function (index) {
        debugger;
        $scope.matItems.splice(index, 1);
    }
    /////////////////////////////////////////////////////////

    ////////////////////////////////////////////////////////////

    $scope.tmenuid = "";
    $scope.tsmenuid = "";
    $scope.tmenu = "";
    $scope.tsmenu = "";


    $scope.userItems = [];
    $scope.usItem = function (menuid,menu, smenuid,smenu) {
        debugger;

        $scope.userItems.push({
            tmenuid: menuid,
            tsmenuid: smenuid,
            tmenu: menu,
            tsmenu: smenu,
        });
        $scope.menuid = "";
        $scope.smenuid = "";
        $scope.menu = "";
        $scope.smenu= "";

    }

    $scope.usremoveItem = function (index) {
        debugger;
        $scope.userItems.splice(index, 1);
    }
    /////////////////////////////////////////////////////////


    $scope.cevntdt = "";
    $scope.cnbenef = "";
    $scope.cdgid = "";
    $scope.cmc_catid = "";
    $scope.cclmamn = "";
    $scope.doamnt = "";
    $scope.authy = "";
    $scope.mob = "";

     $scope.clitems = [];
     $scope.claddItem = function (tevntdt,tnbenef,tdgid,tmc_catid,tclmamnt,tdoamnt,tmob,tauthy) {
        debugger;
        if (tevntdt == '' || tevntdt == undefined) {
            $("#errormsgspannurse").text("Please Enter Event Date");
            //return false;
        } else if (tnbenef == '' || tnbenef == undefined) {
            $("#errormsgspannurse").text("Please Enter Name");
            //return false;
        } else if (tmob == '' || tmob == undefined) {
            $("#errormsgspannurse").text("Please Enter Phone Number");
            //return false;
        }
        else if (!isValidPhone(tmob)) {
            $("#errormsgspannurse").text("Please Enter Correct Phone Number");
            //return false;
        }
        else if (tauthy == '' || tauthy == undefined) {
            $("#errormsgspannurse").text("Please Enter Authority");
            //return false;
        }
        else if (tdgid == '' || tdgid == undefined) {
            $("#errormsgspannurse").text("Please Select Diagnosis");
            //return false;
        }
        else if (tmc_catid == '' || tmc_catid == undefined) {
            $("#errormsgspannurse").text("Please Select Catagory");
            //return false;
        }
        else if (tclmamnt == '' || tclmamnt == undefined) {
            $("#errormsgspannurse").text("Please Enter Claim Amount");
            //return false;
        }
       
       
        else {

            document.getElementById("dgcat").disabled = true;
          //  $("#svd").removeAttr('class');
            $("#errormsgspannurse").text(" ");
            $scope.clitems.push({
                cevntdt: tevntdt,
                cnbenef: tnbenef,
                cdgid: tdgid,
                cmc_catid: tmc_catid,
                cclmamn: tclmamnt,
                doamnt: tdoamnt,
                authy: tauthy,
                mob: tmob ,
            });
            $scope.tevntdt = "";
            $scope.tnbenef = "";
            $scope.tdgid = "";
            $scope.tmc_catid = "";
            $scope.tclmamnt = "";
            $scope.tdoamnt = "";
            $scope.tauthy = "";
            $scope.tmob = "";
        }
    }

    $scope.clremoveItem = function (index) {
        debugger;
        $scope.clitems.splice(index, 1);
    }

    function isValidPhone(phoneNumber) {
       
        phoneNumber = '0' + phoneNumber.toString().replace(/[^0-9]/g, '');

        var l = phoneNumber.length;
            if (l < 10 && !isNaN(phoneNumber)) { return false; }
            
            else {
                return true;
            }


        
    }

    $scope.dgid = "";
    $scope.dgdet = "";


    $scope.bitems = [];
    $scope.baddItem = function (dgid1, dgdet1) {
        debugger;

        $scope.bitems.push({
            dgid: dgid1,
            dgdet: dgdet1,



        });
        $scope.dgid1 = "";
        $scope.dgdet1 = "";


    }

    $scope.bremoveItem = function (index) {
        debugger;
        $scope.bitems.splice(index, 1);
    }

    $scope.saveditem = function () {
        debugger;
        var getData = EpisodeService.saveditem($scope.DName1, $scope.qnty, $scope.dordno);

        $scope.showerror = false; getData.then(function (pderror) {

            $scope.submiterror = pderror.data;
            //alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.smsdesc = function (index1, index2) {
        debugger;
        $scope.smsdes = index1;
    }
   
    $scope.medkn = "";


    $scope.mitems = [];
    $scope.maddItem = function (mkno) {
        debugger;
        if ($scope.Present_Complain != undefined) {
            $scope.Present_Complain = $scope.Present_Complain +" "+ mkno;
        }
        else {
            $scope.Present_Complain =  mkno;
        }
        $scope.medkey1 = "";
        $scope.mitems.push({
            medkn: mkno,
           



        });
        $scope.mkno = "";
     


    }
    $scope.pmedkn = "";
    $scope.pmitems = [];
    $scope.pmaddItem = function (pmkno) {
        debugger;
        if ($scope.pastmedhys != undefined) {
            $scope.pastmedhys = $scope.pastmedhys + " " + pmkno;
        }
        else {
            $scope.pastmedhys = pmkno;
        }
        $scope.pmedkey1 = "";
        $scope.pmitems.push({
            pmedkn: pmkno,




        });
        $scope.pmkno = "";



    }
    $scope.dmedkn = "";
    $scope.dmitems = [];
    $scope.dmaddItem = function (dmkno) {
        debugger;
        if ($scope.drughyst != undefined) {
            $scope.drughyst = $scope.drughyst + " " + dmkno;
        }
        else {
            $scope.drughyst = dmkno;
        }
        $scope.dmedkey1 = "";
        $scope.dmitems.push({
           dmedkn: dmkno,




        });
        $scope.dmkno = "";



    }


    $scope.Slabid = "";
    $scope.Slabcat = "";
    $scope.Ssmsval = "";

    $scope.Slitems = [];
    $scope.SladdItem = function (Stlabid, Stlabcat, Stsmsval) {
        debugger;
        if (Stlabid == 21) {

            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,
                Ssmsval: ' value is ' + Stsmsval + ' mgdl',


            });
        }
        else if (Stlabid == 22) {

            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,
                Ssmsval: ' value is ' + Stsmsval + ' %',


            });
        }
        else if (Stlabid == 23) {

            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,
                Ssmsval: ' value is ' + Stsmsval + ' mgdl',


            });
        }
        else if (Stlabid == 24) {

            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,
                Ssmsval: ' value is ' + Stsmsval + ' mgdl',


            });
        }
        else if (Stlabid == 25) {

            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,
                Ssmsval: ' value is ' + Stsmsval + ' mgdl',


            });
        }
        else {
            $scope.Slitems.push({
                Slabid: Stlabid,
                Slabcat: Stlabcat,



            });


        }
        $scope.Stlabid = "";
        $scope.Stlabcat = "";
        $scope.Stsmsval = "";
        $scope.runsmsfun();
    }

    $scope.runsmsfun = function () {
        debugger;

        $scope.smstext = 'Your ' + $('#smsdiv').text().trim() + ' report is ready to be collected  Hosp. CBO';
    }

    $scope.SlremoveItem = function (index) {
        debugger;

        $scope.Slitems.splice(index, 1);
    }
    $scope.scatid = "";
    $scope.scategory = "";
    $scope.sdays = "";
    $scope.seff = "";

    $scope.sitems = [];
    $scope.saddItem = function (scatid1, scategory1, sdays1, seff1) {
        debugger;

        if ((scatid1 != '12' && scatid1 != '13' && scatid1 != '1' && scatid1 != '16' && scatid1 != '17') && (sdays1 == '' || sdays1 == undefined)) {
            $("#errormsgspan1").text("Please Add no of Days");
            //return false;
        } else if (seff1 == '' || seff1 == undefined) {
            $("#errormsgspan1").text("Please Select Effective Date");
            //return false;
        }
       
        else {
            $("#errormsgspan1").text(" ");


            $scope.sitems.push({
                scatid: scatid1,
                scategory: scategory1,
                seff: seff1,
                sdays: sdays1
            });
            $scope.scatid1 = "";
            $scope.scategory1 = "";
            $scope.sdays1 = "";
            $scope.seff1 = "";
        }
    }

    $scope.sremoveItem = function (index) {
        debugger;
        $scope.sitems.splice(index, 1);
    }

    $scope.submitvitals = function (ite) {
        debugger;
        var getData = EpisodeService.submitvital(ite);

        $scope.showerror = false; getData.then(function (hms) {


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    $scope.loadchild = function (id) {
        debugger;
      
        $scope.GetPatientDet[0] = id;

      
    }
    $scope.loadchildclaim = function () {
        debugger;

       // $scope.GetPatientDet[0] = id;
        if ($scope.GetPatientDet[0].Initials != null) {
            $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;
        }
        else {
            $scope.nbenef = $scope.GetPatientDet[0].Surname;
        }

    }
    $scope.loadchild1 = function (id) {
        debugger;



        var getData = EpisodeService.loadchildimg1(id);
        $scope.showerror = false; getData.then(function (model) {
            debugger;



            $scope.primage1 = model.data;

        }, function () {
            //alert('Error in getting records-loadchild');
        });

    }
    $scope.loadchild2 = function (id) {
        debugger;



        var getData = EpisodeService.loadchildimg1(id);
        $scope.showerror = false; getData.then(function (model) {
            debugger;



            $scope.primage = model.data;

        }, function () {
            //alert('Error in getting records-loadchild');
        });

    }
    $scope.loadimgbystp = function (stp,id) {
        debugger;



        var getData = EpisodeService.loadimgbystp(stp,id);
        $scope.showerror = false; getData.then(function (model) {
            debugger;



            $scope.primage = model.data;

        }, function () {
            //alert('Error in getting records-loadchild');
        });

    }
    $scope.viewsick = function (id) {

        $scope.viewsickr
        var getData = EpisodeService.viewsick1(id);
        $scope.viewsickr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.viewsickr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
   
    $scope.getdty = function (id) {
        debugger;
        var date = new Date(parseInt(id.substr(6)));
        $scope.dtofb = date.toLocaleDateString("en-GB");

    }
    
    $scope.getses = function (id) {
        debugger;
        if (id == "M") {
            $scope.sexselect = 0;
        }
        if (id== "F") {
            $scope.sexselect = 1;
        }
        $scope.getsxs = $scope.sxs[$scope.sexselect];
    }
    
    $scope.setprd = function (id) {
        debugger;
        
        $scope.prced = id;

    }
    $scope.getchno = function (id) {
        debugger;
       
        for (i = 0; i < $scope.childet.length; i++) {
            var sd = $scope.childet[i];
            if (id == $scope.childet[i].DOB) {
                $scope.chno = i+1;
            }
        }
        
        
    }


    $scope.Getserviceno = function (id) {
        debugger;
        $scope.rankselect
        $scope.sexselect
        $scope.ServicePersonnels
        $scope.childet
        $scope.dtofb
        var getData = EpisodeService.getsp(id);

        $scope.ServicePersonnels = null;
        $scope.rankselect = null;
        $scope.childet = null;
        $scope.dtofb = new Date;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            //$scope.Surname = hms.data[0].Surname;
            $scope.ServicePersonnels = hms.data.result;
            $scope.childet = hms.data.result1;
            if ($scope.ServicePersonnels[0] != null) {

                $scope.rankselect = $scope.ServicePersonnels[0].Rank;


                if ($scope.ServicePersonnels[0].Sex == "1") {
                    $scope.sexselect = 0;
                }
                if ($scope.ServicePersonnels[0].Sex == "0") {
                    $scope.sexselect = 1;
                }
                $scope.ranksa = $scope.rnks[$scope.rankselect];
                
                $scope.getsxs = $scope.sxs[$scope.sexselect];
                var dt = $scope.ServicePersonnels[0].DateOfBirth;
                var date = new Date(parseInt(dt.substr(6)));
                $scope.dtofb = date.toLocaleDateString("en-GB");
                // $scope.ServicePersonnels.Rank;
                // $scope.ServicePersonnels.Rank = 2;
                //alert($scope.ServicePersonnels[0].Surname);
                //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
                //$scope.rankname = Personnel.RNK_NAME;
                //$scope.surname = Personnel.ActiveNo;
                // $scope.WefDate = $scope.por[0].WefDate;
            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getservicenoedt = function (id) {
        debugger;
        $scope.rankselect
        $scope.sexselect
        $scope.ServicePersonnels
        $scope.childet
        $scope.dtofb
        var getData = EpisodeService.Getservicenoedt(id);

        $scope.ServicePersonnels = null;
        $scope.rankselect = null;
        $scope.childet = null;
       
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            //$scope.Surname = hms.data[0].Surname;
            $scope.ServicePersonnels = hms.data.result;
            $scope.childet = hms.data.result1;
            if ($scope.ServicePersonnels[0] != null) {

             
                $scope.Surname = $scope.ServicePersonnels[0].Surname;

                $scope.getdty($scope.ServicePersonnels[0].DateOfBirth);
                // $scope.ServicePersonnels.Rank;
                // $scope.ServicePersonnels.Rank = 2;
                //alert($scope.ServicePersonnels[0].Surname);
                //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
                //$scope.rankname = Personnel.RNK_NAME;
                //$scope.surname = Personnel.ActiveNo;
                // $scope.WefDate = $scope.por[0].WefDate;
            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getrnks = function () {

        var getData = EpisodeService.getrnks();

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.rnks = model.data;


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }
    $scope.getsex = function () {

        var getData = EpisodeService.getsex();

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.sxs = model.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }


    $scope.getrelations = function () {
        $scope.getrelet

        var getData = EpisodeService.getrelation();
        $scope.getrelet = null;

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.getrelet = model.data;
            $scope.relet = $scope.getrelet[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }
    //To Get All Records 
    $scope.GetAllPOR = function (id) {
        debugger;

        $scope.Action = "View";
        ClearDiv();
        $scope.divPORlist = true;
        $scope.PorHeader1.PorCat = $scope.PorHeader1.trim();

        id = $scope.LocationID + "-" + $scope.PorHeader1 + "-" + $scope.Year + "-" + $scope.PorNo;
        //GetPorDtls(id);
        var getData = EpisodeService.getPors(id);

        $scope.showerror = false; getData.then(function (por) {
            $scope.pors = por.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

        // Get wef date
        //var getPORData = EpisodeService.getPOR(id);

        //getPORData.then(function (porD) {

        //    $scope.PORD = porD.data;
        //    $scope.WefDate = $scope.PORD[0].WefDate;
        //    $scope.Year = $scope.PORD[0].Year;
        //    //$scope.surname = Personnel.ActiveNo;

        //}, function () {
        //    $scope.showloader = false;  $scope.showerror =true; 
        //});


    }

    $scope.GetPorDtls = function (id) {
        debugger;
        var getData = EpisodeService.getPorDtls(id);

        $scope.showerror = false; getData.then(function (por) {

            $scope.POR = por.data;
            //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
            //$scope.rankname = Personnel.RNK_NAME;
            //$scope.surname = Personnel.ActiveNo;
            // $scope.WefDate = $scope.por[0].WefDate;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetVitalType() {
        debugger;
        var getData = EpisodeService.GetVitalTypes();

        $scope.showerror = false; getData.then(function (VitalType) {
            $scope.VitalTypes = VitalType.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetpatiantType() {
        debugger;
        var getData = EpisodeService.GetpatiantType();

        $scope.showerror = false; getData.then(function (GetpatiantType) {
            $scope.GetpatiantTypes = GetpatiantType.data;
            $scope.GetpatiantTypes1 = $scope.GetpatiantTypes[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetpatiantsubType() {
       
        var getData = EpisodeService.GetpatiantsubType();

        $scope.showerror = false; getData.then(function (GetpatiantsubType) {
         debugger;
         $scope.GetpatiantsubTypes = GetpatiantsubType.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetSeverityTypes() {
        debugger;
        var getData = EpisodeService.GetSeverityTypes();

        $scope.showerror = false; getData.then(function (SeverityTypes) {
            $scope.SeverityType = SeverityTypes.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetRoute() {
        debugger;
        var getData = EpisodeService.GetRoute();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.Route3 = mod.data;
            $scope.Route2 = $scope.Route3[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetSt() {
        debugger;
        var getData = EpisodeService.GetSt();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.patientstatus = mod.data;
            $scope.pntstatus = $scope.patientstatus[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetStatusWard() {
        debugger;
        var getData = EpisodeService.GetStatusWard();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.patientstatus = mod.data;
            $scope.pntstatus = $scope.patientstatus[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetWardTypes() {
       
        var getData = EpisodeService.GetWardTypes();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.wardTypes = mod.data;
            $scope.wardNo = $scope.wardTypes[1];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    function Getdgn() {
        debugger;
        var getData = EpisodeService.Getdgn();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.Getdgn1 = mod.data;
           // $scope.dgn1 = $scope.Getdgn1[0];

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     function Getclmcat() {
        debugger;
        var getData = EpisodeService.Getclmcat();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.Getclmcat1 = mod.data;
           // $scope.dgn1 = $scope.Getdgn1[0];

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetClinics() {
        debugger;
        var getData = EpisodeService.GetClinics();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.GetClinic = mod.data;
            // $scope.pntstatus = $scope.patientstatus[4];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetMethod() {
      
        var getData = EpisodeService.GetMethod();

        $scope.showerror = false; getData.then(function (mod1) {
            debugger;
            $scope.Method3 = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getmes() {

        var getData = EpisodeService.Getmes();

        $scope.showerror = false; getData.then(function (mod1) {
            debugger;
            $scope.medmes = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsnp() {
        debugger;
        var getData = EpisodeService.Getsnp();

        $scope.showerror = false; getData.then(function (mod1) {
            $scope.nop3 = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getnut() {
        debugger;
        var getData = EpisodeService.Getnut();

        $scope.showerror = false; getData.then(function (mod1) {
            $scope.nutr3 = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsut() {
        debugger;
        var getData = EpisodeService.Getsut();

        $scope.showerror = false; getData.then(function (mod1) {
            $scope.sutm3 = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsta() {
        debugger;
        var getData = EpisodeService.Getsta();

        $scope.showerror = false; getData.then(function (mod1) {
            $scope.toa3 = mod1.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetDrug() {
        debugger;
        var getData = EpisodeService.GetDrug();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.DName = mod.data;
            $scope.states = $scope.DName.itemdescription;
            localStorage.setItem("dlist1", JSON.stringify($scope.DName));
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetDrugWard() {
        debugger;
        var getData = EpisodeService.GetDrugWard();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.DName = mod.data;
            $scope.states = $scope.DName.itemdescription;
            localStorage.setItem("dlistWard", JSON.stringify($scope.DName));
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetDrugtime() {
        debugger;
        var getData = EpisodeService.GetDrugtime();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.dtime3 = mod.data;
            $scope.dtime2 = $scope.dtime3[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getperdose() {
        debugger;
        var getData = EpisodeService.Getperdose();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.Dose3 = mod.data;
         
            $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurmo() {
        debugger;
        var getData = EpisodeService.Getsurmo();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surmo = mod.data;

          //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurasi() {
        debugger;
        var getData = EpisodeService.Getsurasi();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surasi = mod.data;

            //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getatsur() {
        debugger;
        var getData = EpisodeService.Getatsur();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.atsur = mod.data;

            $scope.attsrg = $scope.atsur[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function Getloc() {
        debugger;
        var getData = EpisodeService.Getloc();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.locc = mod.data;

            $scope.loc = $scope.locc[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    
   function Getphloc ()  {

        $scope.Getuserdept1
        $scope.showloader = true;
        var getData = EpisodeService.Getphloc();
        $scope.Getuserdept1 = null;

        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            $scope.Getuserdept1 = hms.data;
            $scope.showloader = false; $scope.showerror = true;


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function Getcltyp() {
        debugger;
        var getData = EpisodeService.Getcltyp();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.cltp = mod.data;

            $scope.cltyp = $scope.cltp[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getdiv = function (id) {
        debugger;


        var getData = EpisodeService.Getdiv($scope.loc.LocationID, $scope.cltyp.ClinicTypeID);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.divv = subcategory.data;
            $scope.div = $scope.divv[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
   
        function Getmmenu() {

        var getData = EpisodeService.Getmmenu();

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.mmenu = subcategory.data;
            $scope.menu = $scope.mmenu[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getsubmmenu = function (id) {
        debugger;


        var getData = EpisodeService.Getsubmmenu(id);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.smmenu = subcategory.data;
            $scope.smenu = $scope.smmenu[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Getdivnurs = function (id) {
        debugger;


        var getData = EpisodeService.Getdivnurs(id.LocationID);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.divv = subcategory.data;
            $scope.div = $scope.divv[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurfeq() {
        debugger;
        var getData = EpisodeService.Getsurfeq();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surfeq = mod.data;

            //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurpdur() {
        debugger;
        var getData = EpisodeService.Getsurpdur();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surpdur = mod.data;

            //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurtecniq() {
        debugger;
        var getData = EpisodeService.Getsurtecniq();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surteq = mod.data;

            //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsurpom() {
        debugger;
        var getData = EpisodeService.Getsurpom();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.surpom = mod.data;
           // $scope.pom1 = $scope.surpom[0];
            //  $scope.Dose = $scope.Dose3[3];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     $scope.PatientHystoryfp = function (id) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystoryfp(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {

                $scope.h1 = model.data.h1;
                $mybook = $("#mybook"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetMedkw() {
        debugger;
        var getData = EpisodeService.GetMedkw();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.medkey = mod.data;
           // $scope.states = $scope.DName.itemdescription;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getlablist() {
        debugger;
        var getData = EpisodeService.Getlablist();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.lName = mod.data;
            $scope.lstates = $scope.lName.CategoryName;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function Getsmslablist() {
        debugger;
        var getData = EpisodeService.Getsmslablist();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.slName = mod.data;
            $scope.slstates = $scope.slName.CatName;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetSicktype() {
        debugger;
        var getData = EpisodeService.GetSicktype();

        $scope.showerror = false; getData.then(function (mod) {
            $scope.CName = mod.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetHyperMainType() {
        debugger;
        var getData = EpisodeService.GetHyperMainType();

        $scope.showerror = false; getData.then(function (HyperTypes) {
            $scope.HyperType = HyperTypes.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetHyperReactType() {
        debugger;
        var getData = EpisodeService.GetHyperReactType();

        $scope.showerror = false; getData.then(function (HyperReact) {
            $scope.HypRMainCategory = HyperReact.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.GetHyperSubType = function (id) {
        debugger;


        var getData = EpisodeService.GetHyperSubType1($scope.HyperType1.HypersenceMainCatID);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.HyperSubType = subcategory.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.GetReactSubType = function (id) {
        debugger;


        var getData = EpisodeService.GetReactSubType($scope.HypRMainCategory1.HypRMainID);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.HypRSubCategory = subcategory.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Getuserdep = function (id) {

        $scope.Getuserdept1
        $scope.showloader = true;
        var getData = EpisodeService.Getuserdept(id);
        $scope.Getuserdept1 = null;
        $scope.subloc1 = null;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            $scope.Getuserdept1 = hms.data;
            $scope.showloader = false; $scope.showerror = true;


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.loadsubloc = function () {

        $scope.subloc1
        $scope.showloader = true;
        var getData = EpisodeService.loadsubloc($scope.loclisi.Clinic_ID);
        $scope.subloc1 = null;
        $scope.subloc12 = null;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            
            $scope.subloc1 = hms.data;
            $scope.showloader = false; $scope.showerror = true;


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.loginuser = function () {

        $scope.retuser
        $scope.showloader = true;

        $scope.finaldiv;
        if ($scope.subloc12!=null) {
            $scope.finaldiv = $scope.subloc12.DivisionID;
        }
        else {
            $scope.finaldiv = "";
        }
        var getData = EpisodeService.loginuser($scope.username, $scope.passwd, $scope.loclisi.Clinic_ID, $scope.finaldiv);
        $scope.subloc1 = null;

        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            $scope.retuser = hms.data;
            $scope.showloader = false; $scope.showerror = true;
            if ($scope.retuser == 'ok') {
                window.location.href = "../Home/Index";
               
            }
            else {
                $("#errormsgspan").text($scope.retuser);
            }

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }



    $scope.GetPatientnew = function (st,id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        $scope.Relationship = $scope.Relationships[0];
        $scope.ServiceNo = id;
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            $scope.showloader = false;
        }
        else {

            var getData = EpisodeService.GetPatients(st, id, relet);
            $scope.GetPatientDet = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try {
                    $scope.GetPatientDet = hms.data.serv;
                    $scope.PatientDetdr = hms.data.serv;
                    $scope.patienthsthyp1 = hms.data.serv1;
                    if ($scope.GetPatientDet != "") {
                        $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;
                    }
                   

                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
                  else  if ($scope.submiterror == '2') {
                        window.location.href = "../users/login";
                    }
                  else  if ($scope.submiterror == '3') {
                        $("#errormsgspannurse").text("Details are not in HRMS");
                    }
                   else if ($scope.submiterror == '4') {
                        $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                    }
                    else if ($scope.submiterror == '5') {
                        $("#errormsgspannurse").text("Black Listed Person in Air Force!");
                    }
                    else {
                        $("#errormsgspannurse").text("");
                    }
                    $scope.showloader = false; $scope.showerror = true;
                }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
                }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }

    $scope.GetsurgatientDischarj = function (id, id2) {
        debugger;
        $scope.showloader = true;

        var getData = EpisodeService.GetsurgatientDischarj(id, id2);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.surghst = model.data.s1;
            $scope.surgthsthyp = model.data.l1;
            $scope.mgtList = model.data.k1;
            $scope.CatList = model.data.c1;
            $scope.druglistdd = model.data.dd;
            $scope.Relationship = $scope.surghst[0].Relationship;
            $scope.pid = $scope.surghst[0].pid;
            $scope.ServiceNo = $scope.surghst[0].ServiceNo;
            $scope.doa = $scope.surghst[0].AdmitDate;
            $scope.dod = $scope.surghst[0].DisDate;
            $scope.dgn1 = model.data.da;
            $scope.pcomp = $scope.surghst[0].Present_Complain;
            $scope.hpcomp = $scope.surghst[0].History_OtherComplain;
            $scope.pastmed = $scope.surghst[0].History_PresentComplain;
            $scope.pastsurg = $scope.surghst[0].Other_Complain;
            $scope.allgi = $scope.surghst[0].Other_Complain;
            $scope.pdid = $scope.surghst[0].PDID; 
            $scope.bhtNo = $scope.surghst[0].BHTNo;


            $scope.mgh = model.data.k1;
            $scope.allgi = model.data.m1;
            $scope.cat = model.data.c1;

            $scope.submiterror = model.data.err;
            if ($scope.submiterror == '1') {
                alert('Error!');
            }
            if ($scope.submiterror == '2') {
                window.location.href = "../users/login";
            }
            if ($scope.submiterror == '3') {
                $("#errormsgspandoc").text("Details are not in HRMS");
            }
            else {
                $("#errormsgspandoc").text("");
            }
            $scope.showloader = false; $scope.showerror = true;
        
}, function () {
    $scope.showloader = false; $scope.showerror = true;
});
}

    $scope.GetPatientlab = function (stp, id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            $scope.showloader = false;
        }
        else {

            var getData = EpisodeService.GetPatientlab(stp, id, relet);
            $scope.GetPatientDet = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try {
                    $scope.GetPatientDet = hms.data.serv;
                    $scope.PatientDetdr = hms.data.serv;
                    $scope.patienthsthyp1 = hms.data.serv1;
                    if ($scope.GetPatientDet != "") {
                        if ($scope.GetPatientDet[0].Initials!=null) {
                            $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;
                        }
                        else {
                            $scope.nbenef = $scope.GetPatientDet[0].Surname;
                        }

                        var dt = $scope.GetPatientDet[0].dob;

                        const date1 = new Date(parseInt(dt.substr(6)));
                        const date2 = new Date();

                        $scope.age = calcDate(date2, date1);

                    }
                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
                    else if ($scope.submiterror == '2') {
                        // window.location.href = "../users/login";
                        $("#errormsgspannurse").text("Error Occured please login!");
                    }
                    else if ($scope.submiterror == '3') {
                        alert('Details Not in HRMS!');
                        $("#errormsgspannurse").text("Details are not in HRMS");
                    }
                   else if ($scope.submiterror == '4') {
                        $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                    }
                   else if ($scope.submiterror == '5') {
                        $("#errormsgspannurse").text("Black listed Person in Air Force!");
                    }
                    else {
                        $("#errormsgspannurse").text("");
                    }
                    $scope.showloader = false; $scope.showerror = true;
                }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
                }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }

    $scope.GetPatient = function (stp,id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            $scope.showloader = false;
        }
        else {

            var getData = EpisodeService.GetPatients(stp,id, relet);
            $scope.GetPatientDet = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try {
                    $scope.GetPatientDet = hms.data.serv;
                    $scope.PatientDetdr = hms.data.serv;
                    $scope.patienthsthyp1 = hms.data.serv1;
                    if ($scope.GetPatientDet != "") {
                        $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;

                        var dt = $scope.GetPatientDet[0].dob;

                        const date1 = new Date(parseInt(dt.substr(6)));
                        const date2 = new Date();

                        $scope.age = calcDate(date2, date1);

                    }
                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
                   else if ($scope.submiterror == '2') {
                        
                        alert("Session Expired Login Again !");
                        window.location.href = "../users/login";
                    }
                   else if ($scope.submiterror == '3') {
                        alert('Details Not in HRMS!');
                        $("#errormsgspannurse").text("Details are not in HRMS");
                    }
                   else if ($scope.submiterror == '4') {
                        $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                    }
                   else if ($scope.submiterror == '5') {
                        $("#errormsgspannurse").text("Black Listed Person in Air Force!");
                   }
else if ($scope.submiterror == '6') {
                        $("#errormsgspannurse").text("Retired Persons only Spouse Eligible for health services");
                    }
                    else {
                        $("#errormsgspannurse").text("");
                    }
                    $scope.showloader = false; $scope.showerror = true;
                }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
                }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }
    $scope.GetPatientpurch = function (stp, id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            $scope.showloader = false;
        }
        else {

            var getData = EpisodeService.GetPatientpurch(stp, id, relet);
            $scope.GetPatientDet = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try {
                    $scope.GetPatientDet = hms.data.serv;
                    $scope.PatientDetdr = hms.data.serv;
                    $scope.patienthsthyp1 = hms.data.serv1;
                    if ($scope.GetPatientDet != "") {
                        $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;

                        var dt = $scope.GetPatientDet[0].dob;

                        const date1 = new Date(parseInt(dt.substr(6)));
                        const date2 = new Date();

                        $scope.age = calcDate(date2, date1);

                    }
                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
                    if ($scope.submiterror == '2') {
                        // window.location.href = "../users/login";
                        $("#errormsgspannurse").text("Error Occured please login!");
                    }
                    if ($scope.submiterror == '3') {
                        $("#errormsgspannurse").text("Details are not in HRMS");
                    }
                    if ($scope.submiterror == '4') {
                        $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                    }
                    else {
                        $("#errormsgspannurse").text("");
                    }
                    $scope.showloader = false; $scope.showerror = true;
                }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
                }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }
    $scope.GetPatientsall = function (st,id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            $scope.showloader = false;
        }
        else {

            var getData = EpisodeService.GetPatientsall(st,id, relet);
            $scope.GetPatientDet = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try {
                    $scope.GetPatientDet = hms.data.serv;
                    $scope.PatientDetdr = hms.data.serv;
                    $scope.patienthsthyp1 = hms.data.serv1;
                    $scope.patientmed = hms.data.serv2;
                    if ($scope.GetPatientDet != "") {
                        $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;
                    }
                    if ($scope.patienthsthyp1 != "") {
                        $scope.allgi = $scope.patienthsthyp1[0].HypersenceMainCategory + " " + $scope.patienthsthyp1[0].HypersenseDetail;
                    }
                    if ($scope.patientmed != "") {
                        $scope.pastmed = $scope.patientmed[0].PMHDetail;
                    }
                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
                    if ($scope.submiterror == '2') {
                        window.location.href = "../users/login";
                    }
                    if ($scope.submiterror == '3') {
                        $("#errormsgspannurse").text("Details are not in HRMS");
                    }
                    if ($scope.submiterror == '4') {
                        $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                    }
                    else {
                        $("#errormsgspannurse").text("");
                    }
                    $scope.showloader = false; $scope.showerror = true;
                }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
                }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }
    $scope.GetPatientsick = function (st,id, relet) {
       
        $scope.GetPatientDet1
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        

        var getData = EpisodeService.GetPatientsick(st,id, relet);
            $scope.GetPatientDet1 = null;
            $scope.GetPatientDetdr = null;
            $scope.primage = null;
            $scope.showerror = false; getData.then(function (hms) {
                debugger;
                try{
                $scope.GetPatientDet1 = hms.data.serv;
                $scope.PatientDetdr = hms.data.serv;
                $scope.patienthsthyp1 = hms.data.serv1;
                if ($scope.GetPatientDet1 != "") {
                    $scope.nbenef = $scope.GetPatientDet1[0].Initials + " " + $scope.GetPatientDet1[0].Surname;

                    var dt = $scope.GetPatientDet1[0].dob;

                    const date1 = new Date(parseInt(dt.substr(6)));
                    const date2 = new Date();
                    var dt1 = $scope.GetPatientDet1[0].enl;

                    const date3 = new Date(parseInt(dt1.substr(6)));
                    $scope.age = calcDate(date2, date1);
                    $scope.tservice = calcDate(date2, date3);
                    // $scope.pidnew = $scope.PatientDetdr[0];
                    //$scope.primage = hms.data.imgd;
                    $scope.submiterror = hms.data.err;
                    $("#errormsgspansick").text("");
                    if ($scope.submiterror == '2') {
                        alert("Session Expired Login Again!");
                        window.location.href = "../users/login";
                    }



                }
                else {
                    $scope.submiterror = hms.data.err;
                    if (st == "1" && $scope.submiterror == "") {
                        $("#errormsgspansick").text("No officer for the service no entered!");
                    }
                    else if (st == "2" && $scope.submiterror == "") {
                        $("#errormsgspansick").text("No Airmen for the service no entered!");
                    }

                  
                else if ($scope.submiterror == '1') {
                        alert('Please register First in Patient Registration!');
                    }
            else  if ($scope.submiterror == '2') {
                        window.location.href = "../users/login";
                    }
            else if ($scope.submiterror == '3') {
                        $("#errormsgspansick").text("Details are not in HRMS");
            }
            else if ($scope.submiterror == '5') {
                $("#errormsgspannurse").text("Black Listed Person in Air Force!");
            }
                    else {
                        $("#errormsgspansick").text("");
                    }
                }
                $scope.showloader = false; $scope.showerror = true;

            }
                catch (err) {
                    $scope.showloader = false; $scope.showerror = true;
    }
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        
    }
    $scope.GetPatientmedical = function (st,id, relet) {
       
        $scope.GetPatientDet1
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        

        var getData = EpisodeService.GetPatientmedical(st,id, relet);
        $scope.GetPatientDet = null;
        //$scope.GetPatientDetdr = null;
        //$scope.lablist = null;
        $scope.primage = null;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            try {
                $scope.GetPatientDet = hms.data.serv;
                 $scope.GetPatientDetv = hms.data.meddata;
                 $scope.lablist = hms.data.l1;
                //$scope.PatientDetdr = hms.data.serv;
                //$scope.patienthsthyp1 = hms.data.serv1;
                if ($scope.GetPatientDet != "") {
                    $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;

                    var dt = $scope.GetPatientDet[0].dob;

                    const date1 = new Date(parseInt(dt.substr(6)));
                    const date2 = new Date();
                  
                    $scope.age = calcDate(date2, date1);


                }
                // $scope.pidnew = $scope.PatientDetdr[0];
                //$scope.primage = hms.data.imgd;
                $scope.submiterror = hms.data.err;
                if ($scope.submiterror == '1') {
                    alert('Please register First in Patient Registration!');
                }
                if ($scope.submiterror == '2') {
                    window.location.href = "../users/login";
                }
                if ($scope.submiterror == '3') {
                    $("#errormsgspannurse").text("Details are not in HRMS");
                }
                if ($scope.submiterror == '4') {
                    $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                }
                else {
                    $("#errormsgspannurse").text("");
                }
                $scope.showloader = false; $scope.showerror = true;
            }
            catch (err) {
                $scope.showloader = false; $scope.showerror = true;
            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
        
     }
     $scope.GetPatientsick2 = function (id, relet) {
       
        $scope.GetPatientDet1
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;
        

        var getData = EpisodeService.GetPatients(id, relet);
        $scope.GetPatientDet = null;
        $scope.GetPatientDetdr = null;
        $scope.primage = null;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            try {
                $scope.GetPatientDet = hms.data.serv;
                $scope.PatientDetdr = hms.data.serv;
                $scope.patienthsthyp1 = hms.data.serv1;
                if ($scope.GetPatientDet != "") {
                    $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;

                    var dt = $scope.GetPatientDet[0].dob;

                    const date1 = new Date(parseInt(dt.substr(6)));
                    const date2 = new Date();
                  
                    $scope.age = calcDate(date2, date1);


                }
                // $scope.pidnew = $scope.PatientDetdr[0];
                //$scope.primage = hms.data.imgd;
                $scope.submiterror = hms.data.err;
                if ($scope.submiterror == '1') {
                    alert('Please register First in Patient Registration!');
                }
               else if ($scope.submiterror == '2') {
                    window.location.href = "../users/login";
                }
               else if ($scope.submiterror == '3') {
                    $("#errormsgspannurse").text("Details are not in HRMS");
                }
               else if ($scope.submiterror == '4') {
                    $("#errormsgspannurse").text("Not Submitted Sick Report from Section!");
                }
                else if ($scope.submiterror == '5') {
                    $("#errormsgspannurse").text("Black Listed Person in Air Force!");
                }
                else {
                    $("#errormsgspannurse").text("");
                }
                $scope.showloader = false; $scope.showerror = true;
            }
            catch (err) {
                $scope.showloader = false; $scope.showerror = true;
            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
        
     }
     $scope.GetPatientsick3 = function (id, relet) {

         $scope.GetPatientDet1
         $scope.GetPatientDetdr
         $scope.primage
         $scope.showloader = true;


         var getData = EpisodeService.GetPatientsick3(id, relet);
         $scope.GetPatientDet = null;
         $scope.GetPatientDetdr = null;
         $scope.primage = null;
         $scope.showerror = false; getData.then(function (hms) {
             debugger;
             try {
                 $scope.GetPatientDet = hms.data.serv;
                 $scope.PatientDetdr = hms.data.serv;
                 $scope.meddata = hms.data.serv1;
                 if ($scope.meddata != "") {
                     $scope.age = $scope.meddata[0].age;
                     $scope.bmi = $scope.meddata[0].msbmi;
                     $scope.bp = $scope.meddata[0].msbp;
                     $scope.execg = $scope.meddata[0].msexecg;
                     $scope.fbs = $scope.meddata[0].msfbs;
                     $scope.hbac = $scope.meddata[0].mshbac;
                     $scope.hght = $scope.meddata[0].msheight;
                     $scope.tcol = $scope.meddata[0].mstotalc;
                     $scope.usugar = $scope.meddata[0].msusugar;
                     $scope.vsion = $scope.meddata[0].msvision;
                     $scope.wght = $scope.meddata[0].msweight;
                     $scope.ym = $scope.meddata[0].msyear;
                     $scope.msid = $scope.meddata[0].msid;
                     $scope.dentalst = $scope.meddata[0].dentalst;
                     $scope.msspecs = $scope.meddata[0].msspecs;
                     $scope.pftsession = $scope.meddata[0].pftsession;
                 }
                 if ($scope.GetPatientDet != "") {
                     $scope.nbenef = $scope.GetPatientDet[0].Initials + " " + $scope.GetPatientDet[0].Surname;

                     var dt = $scope.GetPatientDet[0].dob;

                     const date1 = new Date(parseInt(dt.substr(6)));
                     const date2 = new Date();

                     $scope.age = calcDate(date2, date1);


                 }
                 // $scope.pidnew = $scope.PatientDetdr[0];
                 //$scope.primage = hms.data.imgd;
                 $scope.submiterror = hms.data.err;
                 if ($scope.submiterror == '1') {
                     alert('Please register First in Patient Registration!');
                 }
               else  if ($scope.submiterror == '2') {
                     window.location.href = "../users/login";
                 }
              else   if ($scope.submiterror == '3') {
                     $("#errormsgspannurse").text("Details are not in HRMS");
                 }
               else  if ($scope.submiterror == '4') {
                     $("#errormsgspannurse").text("Vision and other info not added by Nurse!");
                 }
                 else if ($scope.submiterror == '5') {
                     $("#errormsgspannurse").text("Black Listed Person in Air Force!");
                 }
                 else {
                     $("#errormsgspannurse").text("");
                 }
                 $scope.showloader = false; $scope.showerror = true;
             }
             catch (err) {
                 $scope.showloader = false; $scope.showerror = true;
             }
         }, function () {
             $scope.showloader = false; $scope.showerror = true;
         });

     }
    function calcDate(date1, date2) {
        var diff = Math.floor(date1.getTime() - date2.getTime());
        var day = 1000 * 60 * 60 * 24;

        var days = Math.floor(diff / day);
        var months = Math.floor(days / 31);
        var years = Math.floor(months / 12);

        var message = years+1;
        

        return message
    }
    ////////////////////////////////////////////
    $scope.viewmsts = function (id, relet,sess1) {
        debugger;
        $scope.msdata
        
        $scope.showloader = true;


        var getData = EpisodeService.viewmsts(id, relet, sess1);
        $scope.msdata = null;
      
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            $scope.msdata = hms.data;
           
           
            $scope.showloader = false; $scope.showerror = true;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    /////////
    $scope.Getuser = function (id, relet) {
        debugger;
        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage
        $scope.showloader = true;


        var getData = EpisodeService.Getuser(id, relet);
        $scope.GetPatientDet = null;
        $scope.GetPatientDetdr = null;
        $scope.primage = null;
        $scope.showerror = false; getData.then(function (hms) {
            debugger;
            $scope.GetPatientDet = hms.data.serv;
            $scope.PatientDetdr = hms.data.serv;
            $scope.patienthsthyp1 = hms.data.serv1;
            if ($scope.GetPatientDet != "") {
                $scope.fname = $scope.GetPatientDet[0].Initials;
                $scope.lname = $scope.GetPatientDet[0].Surname;
            }
            // $scope.pidnew = $scope.PatientDetdr[0];
            //$scope.primage = hms.data.imgd;
            $scope.submiterror = hms.data.err;
            if ($scope.submiterror == '1') {
                alert('Please register First in Patient Registration!');
            }
            if ($scope.submiterror == '2') {
                window.location.href = "../users/login";
            }
            if ($scope.submiterror == '3') {
                $("#errormsgspansick").text("Details are not in HRMS");
            }
            else {
                $("#errormsgspansick").text("");
            }
            $scope.showloader = false; $scope.showerror = true;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.toggle = function () {
        debugger;
        $scope.myVar = !$scope.myVar;
    };
    $scope.Getsurgpatient = function (id) {
        debugger;
        $scope.showloader = true;
       
        var getData = EpisodeService.Getsurgpatient(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.surghst = model.data.s1;
            $scope.surgthsthyp = model.data.l1;

         
            $scope.submiterror = model.data.err;
            if ($scope.submiterror == '1') {
                alert('Error!');
            }
            if ($scope.submiterror == '2') {
                window.location.href = "../users/login";
            }
            if ($scope.submiterror == '3') {
                $("#errormsgspandoc").text("Details are not in HRMS");
            }
            else {
                $("#errormsgspandoc").text("");
            }
            
          
            $scope.showloader = false; $scope.showerror = true;
        }, function () {
            //alert('Error in getting records -GetOPDPatient');
        });
    }

    $scope.GetOPDPatientWard = function (id) {
        debugger;
        $scope.showloader = true;
        $scope.lablist = "";
        $scope.druglist = "";
        $scope.siclist = "";
        $scope.epasonr = "";
        $scope.planofmgt = "";
        $scope.reftohosp = "";
        $scope.pastmedhys = "";
        $scope.planlist = "";
        $scope.wardlist = "";
        $scope.ComList = "";

        
        $scope.drughyst = "";
        $scope.AbdOther = "";
        var getData = EpisodeService.GetOPDPatientWard(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.patienthst = model.data.b1;
            $scope.patienthsthyp = model.data.b2;

            $scope.acrw = model.data.acrw;
            $scope.patienthstall = model.data.b4;
            $scope.patienthstnew = model.data.b5;
            $scope.patientvit = model.data.vitval1;
            $scope.medbrd = model.data.m1;
            $scope.lablist = model.data.l1;
            $scope.druglist = model.data.d1;
            $scope.siclist = model.data.s1;
            $scope.famlist = model.data.u1;
            $scope.planlist = model.data.p1;
            $scope.wardlist = model.data.w1;
            $scope.ComList = model.data.pt;
            $scope.submiterror = model.data.err;
            if ($scope.submiterror == '1') {
                alert('Error!');
            }
            if ($scope.submiterror == '2') {
                alert("Error Occured Please Login Again!");
                 window.location.href = "../users/login";
            }
            if ($scope.submiterror == '3') {
                $("#errormsgspandoc").text("Details are not in HRMS");
            }
            else {
                $("#errormsgspandoc").text("");
            }
            $scope.Present_Complain = $scope.patienthstall[0].Present_Complain;

            var dt = $scope.patienthst[0].dob;

            const date1 = new Date(parseInt(dt.substr(6)));
            const date2 = new Date();

            $scope.age = calcDate(date2, date1);



            if ($scope.patienthstall[0].History_PresentComplain != 'null') {
                $scope.History_PresentComplain = $scope.patienthstall[0].History_PresentComplain;
            }
            $scope.remarks = $scope.wardlist[0].remarks;
            //$scope.wardNo = $scope.wardlist[0].wardNo;
            $scope.bedNo = $scope.wardlist[0].bedNo;
            $scope.BHTNo = $scope.wardlist[0].BHTNo;
            $scope.OPD_Diagnosis = $scope.wardlist[0].Diagnosis;
            $scope.pntStatus = $scope.patientstatus[($scope.wardlist[0].pntStatus) - 1];
            $scope.wardNo = $scope.wardTypes[($scope.wardlist[0].wardNo) - 1];
          
            $scope.Other_Complain = $scope.patienthstall[0].Other_Complain;
            //$scope.OPD_Diagnosis = $scope.patienthstall[0].OPD_Diagnosis;
            if ($scope.patienthstnew[0] != null) {
                if ($scope.patienthstnew[0].PMHDetail != 'null') {
                    $scope.pastmedhys = $scope.patienthstnew[0].PMHDetail;
                }
                if ($scope.patienthstnew[0].ReffNote != 'null') {
                    $scope.reftohosp = $scope.patienthstnew[0].ReffNote;
                }
                if ($scope.patienthstnew[0].PlanofMgt != 'null') {
                    $scope.planofmgt = $scope.patienthstnew[0].PlanofMgt;
                }
                if ($scope.patienthstnew[0].Drughst != 'null') {
                    $scope.drughyst = $scope.patienthstnew[0].Drughst;
                }
                if ($scope.patienthstnew[0].abdx != 'null') {
                    $scope.abdex = $scope.patienthstnew[0].abdx;
                }
                if ($scope.patienthstnew[0].cardx != 'null') {
                    $scope.cardex = $scope.patienthstnew[0].cardx;
                }
                if ($scope.patienthstnew[0].cenx != 'null') {
                    $scope.cenex = $scope.patienthstnew[0].cenx;
                }
                if ($scope.patienthstnew[0].genx != 'null') {
                    $scope.genex = $scope.patienthstnew[0].genx;
                }
                if ($scope.patienthstnew[0].othx != 'null') {
                    $scope.othex = $scope.patienthstnew[0].othx;
                }
                if ($scope.patienthstnew[0].respx != 'null') {
                    $scope.resex = $scope.patienthstnew[0].respx;
                }
            }
            $scope.drugins = $scope.patienthstall[0].History_OtherComplain;
            $scope.showloader = false; $scope.showerror = true;
        }, function () {
            //alert('Error in getting records -GetOPDPatient');
        });
    }

    $scope.GetOPDPatient = function (id) {
        debugger;
        $scope.showloader = true;
        $scope.lablist = "";
        $scope.druglist = "";
        $scope.siclist = "";
        $scope.epasonr = "";
        $scope.planofmgt = "";
        $scope.reftohosp = "";
        $scope.pastmedhys = "";
        
        $scope.drughyst = "";
        $scope.AbdOther = "";
        var getData = EpisodeService.GetOPDPatient(id);

        
        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.patienthst = model.data.b1;
            $scope.patienthsthyp = model.data.b2;
            
            $scope.acrw = model.data.acrw;
            $scope.patienthstall = model.data.b4;
            $scope.patienthstnew = model.data.b5;
            $scope.patientvit = model.data.vitval1;
            $scope.medbrd = model.data.m1;
            $scope.lablist = model.data.l1;
            $scope.druglist = model.data.d1;
            $scope.rdrl = model.data.rdrl;
            $scope.siclist = model.data.s1;
            $scope.famlist = model.data.u1;
            $scope.submiterror = model.data.err;
            if ($scope.submiterror == '1') {
                alert('Error!');
            }
            if ($scope.submiterror == '2') {
                alert("Error Occured Please Login Again!");
                window.location.href = "../users/login";
            }
            if ($scope.submiterror == '3') {
                $("#errormsgspandoc").text("Details are not in HRMS");
            }
            else {
                $("#errormsgspandoc").text("");
            }
            $scope.Present_Complain = $scope.patienthstall[0].Present_Complain;

            var dt = $scope.patienthst[0].dob;

            const date1 = new Date(parseInt(dt.substr(6)));
            const date2 = new Date();

            $scope.age = calcDate(date2, date1);



            if ($scope.patienthstall[0].History_PresentComplain != 'null') {
                $scope.History_PresentComplain = $scope.patienthstall[0].History_PresentComplain;
            }
            $scope.Other_Complain = $scope.patienthstall[0].Other_Complain;
            $scope.OPD_Diagnosis = $scope.patienthstall[0].OPD_Diagnosis;
            if ($scope.patienthstnew[0] != null) {
                if ($scope.patienthstnew[0].PMHDetail != 'null') {
                    $scope.pastmedhys = $scope.patienthstnew[0].PMHDetail;
                }
                if ($scope.patienthstnew[0].ReffNote != 'null') {
                    $scope.reftohosp = $scope.patienthstnew[0].ReffNote;
                }
                if ($scope.patienthstnew[0].PlanofMgt != 'null') {
                    $scope.planofmgt = $scope.patienthstnew[0].PlanofMgt;
                }
                if ($scope.patienthstnew[0].Drughst != 'null') {
                    $scope.drughyst = $scope.patienthstnew[0].Drughst;
                }
                if ($scope.patienthstnew[0].abdx != 'null') {
                    $scope.abdex = $scope.patienthstnew[0].abdx;
                }
                if ($scope.patienthstnew[0].cardx != 'null') {
                    $scope.cardex = $scope.patienthstnew[0].cardx;
                }
                if ($scope.patienthstnew[0].cenx != 'null') {
                    $scope.cenex = $scope.patienthstnew[0].cenx;
                }
                if ($scope.patienthstnew[0].genx != 'null') {
                    $scope.genex = $scope.patienthstnew[0].genx;
                }
                if ($scope.patienthstnew[0].othx != 'null') {
                    $scope.othex = $scope.patienthstnew[0].othx;
                }
                if ($scope.patienthstnew[0].respx != 'null') {
                    $scope.resex = $scope.patienthstnew[0].respx;
                }
            }
            $scope.drugins = $scope.patienthstall[0].History_OtherComplain;
            $scope.showloader = false; $scope.showerror = true;
        }, function () {
            //alert('Error in getting records -GetOPDPatient');
        });
    }
    $scope.Submitpatient = function () {
        debugger;
        $scope.subidn = '0';
        if ($scope.GetpatiantTypes1.CategoryID == '2' && $scope.GetpatiantsubTypes1 == undefined) {
            $("#errormsgspannurse").text("Please Select Medical Reason!");
            //return false;
        } else if ($scope.Relationship == '-- Select Relationship Type --'||$scope.Relationship ==undefined ) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            //return false;
        }
        else if ($scope.Present_Complain == '' || $scope.Present_Complain == undefined) {
            $("#errormsgspannurse").text("Please Enter Present Complain!");
            //return false;
        }
        else if ( $scope.GetPatientDet[0] == undefined) {
            $("#errormsgspannurse").text("If Family, Details Not in HRMS or Servicemen Sick Report Not Submitted From Section , Fill it Below!");
            //return false;
        }
        else {
            if ($scope.GetpatiantsubTypes1 != undefined) {
                $scope.subidn = $scope.GetpatiantsubTypes1.SubCategoryID;
            }
            document.getElementById("svd").disabled = true;
            $("#svd").removeAttr('class');
            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitpatient($scope.subidn, $scope.GetpatiantTypes1.CategoryID, $scope.items, $scope.hitems, $scope.Present_Complain, $scope.GetPatientDet[0].PID, $scope.HDetail);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                if ($scope.submiterror == '2') {
                    window.location.href = "../users/login";
                } else {
                    alert($scope.submiterror);
                    window.location.reload();
                }
               
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }

    /////
    $scope.Submitgenen = function () {
        debugger;
        $scope.subidn = '0';
        if ($scope.genent == '' || $scope.genent == undefined) {
            $("#errormsgspannurse").text("Please Enter General Entry!");
            //return false;
        }
        
        else {
            if ($scope.GetpatiantsubTypes1 != undefined) {
                $scope.subidn = $scope.GetpatiantsubTypes1.SubCategoryID;
            }
            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitgenen($scope.genent, $scope.patienthst[0].PID);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;

                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }
    ////////////////
    $scope.Submitsurgery = function () {
        debugger;
        $scope.subidn = '0';
       
        if ($scope.dop == '' || $scope.dop == undefined) {
            var datf= new Date();
            $scope.dop = datf;
        }
         if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspan").text("Please Select Relationship!");
            //return false;
        }
        else if ($scope.GetPatientDet == '' || $scope.GetPatientDet == undefined) {
            $("#errormsgspan").text("Please Enter Service No!");
            //return false;
        }

        else {
            
            $("#errormsgspan").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitsurgery($scope.GetPatientDet[0].PID, $scope.doa, $scope.dos, $scope.dod, $scope.tt, $scope.nop, $scope.toa, $scope.ind, $scope.suitems, $scope.attsrg, $scope.aby, $scope.ant, $scope.moa, $scope.sst, $scope.set, $scope.Catheter, $scope.Ivline, $scope.Epidural, $scope.find, $scope.prced, $scope.drins, $scope.matItems, $scope.pomitems, $scope.moins, $scope.nutr, $scope.nutri, $scope.ditems, $scope.spins,$scope.nurs);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;

                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }
    $scope.Submitdischarge = function () {
        debugger;
        $scope.subidn = '';

        if ($scope.dop == '' || $scope.dop == undefined) {
            var datf = new Date();
            $scope.dop = datf;
        }
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            //$("#errormsgspan").text("Please Select Relationship!");
            //return false;
        }
        //else if ($scope.GetPatientDet == '' || $scope.GetPatientDet == undefined) {
        //    //$("#errormsgspan").text("Please Enter Service No!");
        //    //return false;
        //}

        else {

            $("#errormsgspan").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitdischarge($scope.pdid, $scope.doa, $scope.dod, $scope.dgn1, $scope.pcomp, $scope.hpcomp, $scope.pastmed, $scope.pastsurg, $scope.allgi, $scope.mgh, $scope.suitems, $scope.disind, $scope.fupins, $scope.bhtNo, $scope.counsult, $scope.cat, $scope.invSummery);
            debugger;
            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;

                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
    }

    $scope.SavepatientWardNurse = function () {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.SavepatientWardNurse($scope.bitems, $scope.reftohosp, $scope.planofmgt, $scope.drughyst, $scope.hitems, $scope.pastmedhys, $scope.items, $scope.sitems, $scope.litems, $scope.ditems, $scope.Present_Complain, $scope.History_PresentComplain, $scope.Other_Complain, $scope.History_OtherComplain, $scope.AbdOther, $scope.patienthstall[0].PDID, $scope.pntStatus, $scope.GClinic, $scope.dgn1, $scope.genex, $scope.cardex, $scope.cenex, $scope.resex, $scope.othex, $scope.abdex, $scope.drugins, $scope.remarks, $scope.wardNo, $scope.bedNo, $scope.mgtPlan, $scope.BHTNo, $scope.psitemsWard);

        $scope.showerror = false; getData.then(function (pderror) {
            debugger;
            $scope.submiterror = pderror.data;
            $scope.showloader = false; $scope.showerror = true;
            $scope.ditems.splice($scope.ditems);
            $scope.hitems.splice($scope.hitems);
            $scope.litems.splice($scope.litems);
            $scope.items.splice($scope.items);
            $scope.sitems.splice($scope.sitems);
            $scope.bitems.splice($scope.bitems);

            if ($scope.submiterror == '2') {
                        window.location.href = "../users/login";
            } else {
                alert($scope.submiterror);
            }
           

            document.location = $("#tp").attr('href');
            window.location.reload();

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Submitsickpat = function () {
        debugger;
            $("#errormsgspan").text("");
            $scope.showloader = true;
        var getData = EpisodeService.Submitsick($scope.GetPatientDet1[0].PID, $scope.livein, $scope.age, $scope.tservice, $scope.forduty, $scope.defaulter, $scope.tmedexam, $scope.formed);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;

                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        
    }
   
    $scope.Submitdoc = function () {
        debugger;
            $("#errormsgspan").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitdoc($scope.sno, $scope.dname, $scope.fdoc);
            debugger;
            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;

                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        
    }
    $scope.Submitmedex = function () {
        debugger;
        $("#errormsgspan").text("");
        $scope.showloader = true;
        var getData = EpisodeService.Submitmedex($scope.GetPatientDet[0].PID, $scope.age, $scope.hght, $scope.wght, $scope.bmi, $scope.bp, "RE" + $scope.vsion1 + "/LE" + $scope.vsion, $scope.hbac, $scope.usugar, $scope.fbs, $scope.tcol, $scope.execg, $scope.tri, $scope.ldl, $scope.yrr, "RE" + $scope.vsion2 + "/LE" + $scope.vsion3, $scope.sess1);
        debugger;
        $scope.showerror = false; getData.then(function (pderror) {
            $scope.showloader = false; $scope.showerror = true;
            $scope.submiterror = pderror.data;

            alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Submitmedex2 = function () {
        debugger;
        $("#errormsgspan").text("");
        $scope.showloader = true;
        var getData = EpisodeService.Submitmedex2($scope.GetPatientDet[0].PID, $scope.bp, $scope.medmes2.Category, $scope.fit, $scope.msid, $scope.resn, $scope.tcol, $scope.fbs);

        $scope.showerror = false; getData.then(function (pderror) {
            $scope.showloader = false; $scope.showerror = true;
            $scope.submiterror = pderror.data;

            alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Submitsicknurs = function () {
        debugger;
        $("#errormsgspan").text("");
        $scope.showloader = true;
        var getData = EpisodeService.Submitsicknurs($scope.GetPatientDet1[0].PID, $scope.livein, $scope.age, $scope.tservice, $scope.forduty, $scope.defaulter, $scope.loc.LocationID, $scope.div.DivisionID);

        $scope.showerror = false; getData.then(function (pderror) {
            $scope.showloader = false; $scope.showerror = true;
            $scope.submiterror = pderror.data;

            alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Savemenu = function () {
        debugger;
        $("#errormsgspan").text("");
        $scope.showloader = true;
        var getData = EpisodeService.Savemenu($scope.ServiceNo, $scope.loc.LocationID, $scope.div.DivisionID, $scope.cltyp.ClinicTypeID, $scope.menu.MenuID);

        $scope.showerror = false; getData.then(function (pderror) {
            $scope.showloader = false; $scope.showerror = true;
            $scope.submiterror = pderror.data;

            alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    $scope.Saveuser = function () {
        debugger;
        $("#errormsgspan").text("");
        $scope.showloader = true;
        var getData = EpisodeService.Saveuser($scope.ServiceNo, $scope.fname, $scope.lname, $scope.pass, $scope.loc.LocationID, $scope.div.DivisionID, $scope.cltyp.ClinicTypeID, $scope.userItems);

        $scope.showerror = false; getData.then(function (pderror) {
            $scope.showloader = false; $scope.showerror = true;
            $scope.submiterror = pderror.data;

            alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    /////
    $scope.Submitlabpatient = function () {
        debugger;
        $scope.subidn = '0';
        if ($scope.GetpatiantTypes1.CategoryID == '2' && $scope.GetpatiantsubTypes1 == undefined) {
            $("#errormsgspannurse").text("Please Select Medical Reason!");
            //return false;
        } else if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            //return false;
        }
        else if ($scope.GetPatientDet[0] == undefined) {
            $("#errormsgspannurse").text("Not Authorized, Details Not in HRMS!");
            //return false;
        }

        else {
            if ($scope.GetpatiantsubTypes1 != undefined) {
                $scope.subidn = $scope.GetpatiantsubTypes1.SubCategoryID;
            }
            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitlabpatient($scope.subidn, $scope.GetpatiantTypes1.CategoryID, $scope.litems, $scope.Present_Complain, $scope.GetPatientDet[0].PID);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }



    ///
    $scope.Submitdhs = function () {
        debugger;
        $scope.subidn = '0';

        if ($scope.clmdx == 'null' || $scope.clmdx == undefined) {
            $("#errormsgspannurse").text("Please Enter Register No!");
            //return false;
        }

        else {

            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitdhs($scope.cliitems);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }

    /////////////////////////////////////////////////////////////////
    $scope.cid1 = "";
    $scope.clmamnt1 = "";

    $scope.rtrsn1 = "";
    $scope.cliitems = [];
    $scope.addcltsi = function (cid, clmamnt, rtrsn) {
        debugger;


        if (cid) {

            //var exist=false;
            //for(var i=0;i<$scope.items.length;i++){
            //    if ($scope.items[i].Lab_Index1 == Lab_Index) {
            //        exist=true;
            //    }
            //}
            for (var i = 0; i < $scope.cliitems.length; i++) {
                if ($scope.cliitems[i].cid1 == cid) {
                    $scope.cliitems.splice(i, 1);
                }
            }


            //   if (!exist) {
            $scope.cliitems.push({
                cid1: cid,
                clmamnt1: clmamnt,
                rtrsn1: rtrsn

            });
            // }
        }
        $scope.cid = "";
        $scope.clmamnt = "";
        $scope.rtrsn = "";
    }
    //////////////////////////////////////////////////////////////////////
    $scope.cid1 = "";
    $scope.claimcat1 = "";
    $scope.clmamnt1 = "";
    $scope.docamount1 = "";
    $scope.upitems = [];
    $scope.upcltsi = function (cid, claimcat, clmamnt,docamount) {
        debugger;


        if (cid) {

            //var exist=false;
            //for(var i=0;i<$scope.items.length;i++){
            //    if ($scope.items[i].Lab_Index1 == Lab_Index) {
            //        exist=true;
            //    }
            //}
            for (var i = 0; i < $scope.cliitems.length; i++) {
                if ($scope.cliitems[i].cid1 == cid) {
                    $scope.cliitems.splice(i, 1);
                }
            }


            //   if (!exist) {
            $scope.upitems.push({
                cid1: cid,
                claimcat1: claimcat,
                clmamnt1: clmamnt,
                docamount1: docamount,

            });
            // }
        }
        $scope.cid = "";
        $scope.clmamnt = "";
        $scope.docamount = "";
        $scope.claimcat = "";
    }
    //////////////////////////////////////////////////////////////////////
    $scope.Submitclup = function () {
        debugger;
        $scope.subidn = '0';

        if ($scope.clmdx == 'null' || $scope.clmdx == undefined) {
            $("#errormsgspannurse").text("Please Enter Register No!");
            //return false;
        }

        else {

            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitclup($scope.upitems);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }
    ///////////////////////////////////////////////////////////////////////
    $scope.Submittsi = function () {
        debugger;
        $scope.subidn = '0';
        
        if ($scope.clmdx == 'null' || $scope.clmdx == undefined) {
            $("#errormsgspannurse").text("Please Enter Register No!");
            //return false;
        }

        else {

            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submittsi($scope.cliitems);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }
    $scope.Submitgvs = function () {
        debugger;
        $scope.subidn = '0';

        if ($scope.clmdx == 'null' || $scope.clmdx == undefined) {
            $("#errormsgspannurse").text("Please Enter Register No!");
            //return false;
        }

        else {

            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitgvs($scope.cliitems);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }

    $scope.Submitclaim = function () {
        debugger;
        $scope.subidn = '0';
        if ($scope.Relationship == '-- Select Relationship Type --' || $scope.Relationship == undefined) {
            $("#errormsgspannurse").text("Please Select Relationship!");
            //return false;
        }
     else   if ($scope.GetPatientDet == 'null' || $scope.GetPatientDet == undefined) {
            $("#errormsgspannurse").text("Please Enter Service No!");
            //return false;
        }
     else if ($scope.clitems.length == 0 || $scope.clitems == undefined) {
            $("#errormsgspannurse").text("Please Enter claim details!");
            //return false;
        }
      else  if ($scope.chk1 == 'null' || $scope.chk1 == undefined) {
            $("#errormsgspannurse").text("Please complete check list!");
            //return false;
        }
      else if ($scope.chk2 == 'null' || $scope.chk2 == undefined) {
            $("#errormsgspannurse").text("Please complete check list!");
            //return false;
        }
      else if ($scope.chk3 == 'null' || $scope.chk3 == undefined) {
            $("#errormsgspannurse").text("Please complete check list!");
            //return false;
      }
      else if ($scope.chk4 == 'null' || $scope.chk4 == undefined) {
          $("#errormsgspannurse").text("Please complete check list!");
          //return false;
      }
      else if ($scope.chk5 == 'null' || $scope.chk5 == undefined) {
          $("#errormsgspannurse").text("Please complete check list!");
          //return false;
      }
      else if ($scope.chk6 == 'null' || $scope.chk6 == undefined) {
          $("#errormsgspannurse").text("Please complete check list!");
          //return false;
      }
      else if ($scope.chk7 == 'null' || $scope.chk7 == undefined) {
          $("#errormsgspannurse").text("Please complete check list!");
          //return false;
      }
      else if ($scope.chk8 == 'null' || $scope.chk8 == undefined) {
          $("#errormsgspannurse").text("Please complete check list!");
          //return false;
      }
           else if ($scope.chk9 == 'null' || $scope.chk9 == undefined) {
            $("#errormsgspannurse").text("Please complete check list!");
            //return false;
           }
           else if ($scope.chk10 == 'null' || $scope.chk10 == undefined) {
               $("#errormsgspannurse").text("Please complete check list!");
               //return false;
           }
           else if ($scope.chk11 == 'null' || $scope.chk11 == undefined) {
               $("#errormsgspannurse").text("Please complete check list!");
               //return false;
           }
        else {
            
            $("#errormsgspannurse").text("");
            $scope.showloader = true;
            var getData = EpisodeService.Submitclaim($scope.clitems, $scope.GetPatientDet[0].PID);

            $scope.showerror = false; getData.then(function (pderror) {
                $scope.showloader = false; $scope.showerror = true;
                $scope.submiterror = pderror.data;
                alert($scope.submiterror);
                //alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
    }
    $scope.viewlabtest = function (id, catid) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.viewlabtest(id, catid);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.viewlab = model.data;
            $scope.showloader = false;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }
    $scope.acctest = function (id, catid) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.acctest(id, catid);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.viewlab = model.data;
            $scope.showloader = false;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }


    $scope.delPatient = function (id) {
        debugger;
        if (confirm('Are sure You want delete this patient?')) {
            var getData = EpisodeService.delPatient(id);

            $scope.showerror = false; getData.then(function (model) {
                debugger;
                $scope.submiterror = model.data;
                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
           
        }
        else {

        }
       
    }

    $scope.delregdrug = function (id) {
        debugger;
        if (confirm('Are sure You want delete this Drug?')) {
            var getData = EpisodeService.delregdrug(id);

            $scope.showerror = false; getData.then(function (model) {
                debugger;
                $scope.submiterror = model.data;
                alert($scope.submiterror);
                $scope.GetOPDPatient($scope.patienthstall[0].PDID);
               // window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
        else {

        }

    }
    $scope.deldrug = function (id) {
        debugger;
        if (confirm('Are sure You want delete this Drug?')) {
            var getData = EpisodeService.deldrug(id);

            $scope.showerror = false; getData.then(function (model) {
                debugger;
                $scope.submiterror = model.data;
                alert($scope.submiterror);
               // $scope.GetOPDPatient($scope.patienthstall[0].PDID);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
        else {
            
        }

    }

    $scope.deldrugWard = function (id) {
        debugger;
        if (confirm('Are sure You want Remove this Drug?')) {
            var getData = EpisodeService.deldrugWard(id);

            $scope.showerror = false; getData.then(function (model) {
                debugger;
                $scope.submiterror = model.data;
                alert($scope.submiterror);
                window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });
        }
        else {
        }
    }


    $scope.IssuDrug = function (id, id2, id3) {
        debugger;
        if (confirm('Are sure You want Issue this Drug?')) {
            var getData = EpisodeService.IssuDrug(id,id2,id3);

            $scope.showerror = false; getData.then(function (model) {
                debugger;
                $scope.submiterror = model.data;
                alert($scope.submiterror);
               // window.location.reload();
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });

        }
        else {

        }

    }
    $scope.viewlabtest2 = function (id, catid) {
        debugger;

        var getData = EpisodeService.viewlabtest(id, catid);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.viewlab2 = model.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }
    $scope.changeColor1 = function (bool) {

        if (bool == "true") {
            $scope.c1 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c1 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c1 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c1 = { color: 'black' };
        }
    };
    $scope.changeColor2 = function (bool) {

        if (bool == "true") {
            $scope.c2 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c2 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c2 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c2 = { color: 'black' };
        }
    };
    $scope.changeColor3 = function (bool) {

        if (bool == "true") {
            $scope.c3 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c3 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c3 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c3 = { color: 'black' };
        }
    };
    $scope.changeColor4 = function (bool) {
        debugger;
        if (bool == "true") {
            $scope.c4 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c4 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c4 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c4 = { color: 'black' };
        }
    };
    $scope.changeColor5 = function (bool) {

        if (bool == "true") {
            $scope.c5 = { color: 'blue' };
            $scope.c54 = { color: 'black' };
        }
        if (bool == "false" || bool == "") {
            $scope.c5 = { color: 'black' };
            $scope.c54 = { color: 'blue' };
        }
        if (bool === true) {
            $scope.c5 = { color: 'blue' };
            $scope.c54 = { color: 'black' };
        } else if (bool === false) {
            $scope.c5 = { color: 'black' };
            $scope.c54 = { color: 'blue' };
        }
    };
    $scope.changeColor6 = function (bool) {

        if (bool == "true") {
            $scope.c6 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c6 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c6 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c6 = { color: 'black' };
        }
    };
    $scope.changeColor7 = function (bool) {

        if (bool == "true") {
            $scope.c7 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c7 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c7 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c7 = { color: 'black' };
        }
    };
    $scope.changeColor8 = function (bool) {

        if (bool == "true") {
            $scope.c8 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c8 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c8 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c8 = { color: 'black' };
        }
    };
    $scope.changeColor9 = function (bool) {

        if (bool == "true") {
            $scope.c9 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c9 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c9 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c9 = { color: 'black' };
        }
    };
    $scope.changeColor10 = function (bool) {

        if (bool == "true") {
            $scope.c10 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c10 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c10 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c10 = { color: 'black' };
        }
    };
    $scope.changeColor11 = function (bool) {

        if (bool == "true") {
            $scope.c11 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c11 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c11 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c11 = { color: 'black' };
        }
    };
    $scope.changeColor12 = function (bool) {

        if (bool == "true") {
            $scope.c12 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c12 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c12 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c12 = { color: 'black' };
        }
    };
    $scope.changeColor13 = function (bool) {

        if (bool == "true") {
            $scope.c13 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c13 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c13 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c13 = { color: 'black' };
        }
    };
    $scope.changeColor14 = function (bool) {

        if (bool == "true") {
            $scope.c14 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c14 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c14 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c14 = { color: 'black' };
        }
    };
    $scope.changeColor15 = function (bool) {

        if (bool == "true") {
            $scope.c15 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c15 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c15 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c15 = { color: 'black' };
        }
    };
    $scope.changeColor16 = function (bool) {

        if (bool == "true") {
            $scope.c16 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c16 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c16 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c16 = { color: 'black' };
        }
    };
    $scope.changeColor17 = function (bool) {

        if (bool == "true") {
            $scope.c17 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c17 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c17 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c17 = { color: 'black' };
        }
    };
    $scope.changeColor18 = function (bool) {

        if (bool == "true") {
            $scope.c18 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c18 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c18 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c18 = { color: 'black' };
        }
    };
    $scope.changeColor19 = function (bool) {

        if (bool == "true") {
            $scope.c19 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c19 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c19 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c19 = { color: 'black' };
        }
    };
    $scope.changeColor20 = function (bool) {

        if (bool == "true") {
            $scope.c20 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c20 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c20 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c20 = { color: 'black' };
        }
    };
    $scope.changeColor21 = function (bool) {

        if (bool == "true") {
            $scope.c21 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c21 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c21 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c21 = { color: 'black' };
        }
    };
    $scope.changeColor22 = function (bool) {

        if (bool == "true") {
            $scope.c22 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c22 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c22 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c22 = { color: 'black' };
        }
    };
    $scope.changeColor23 = function (bool) {

        if (bool == "true") {
            $scope.c23 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c23 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c23 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c23 = { color: 'black' };
        }
    };
    $scope.changeColor24 = function (bool) {

        if (bool == "true") {
            $scope.c24 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c24 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c24 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c24 = { color: 'black' };
        }
    };
    $scope.changeColor25 = function (bool) {

        if (bool == "true") {
            $scope.c25 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c25 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c25 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c25 = { color: 'black' };
        }
    };
    $scope.changeColor26 = function (bool) {

        if (bool == "true") {
            $scope.c26 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c26 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c26 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c26 = { color: 'black' };
        }
    };
    $scope.changeColor27 = function (bool) {

        if (bool == "true") {
            $scope.c27 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c27 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c27 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c27 = { color: 'black' };
        }
    };
    $scope.changeColor28 = function (bool) {

        if (bool == "true") {
            $scope.c28 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c28 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c28 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c28 = { color: 'black' };
        }
    };
    $scope.changeColor29 = function (bool) {
        debugger;
        if (bool == "true") {
            $scope.c29 = { color: 'blue' };
            $scope.c30 = { color: 'black' };
        }
        if (bool == "false" || bool == "") {
            $scope.c29 = { color: 'black' };
            $scope.c29 = { color: 'blue' };
        }
        if (bool === true) {
            $scope.c29 = { color: 'blue' };
            $scope.c30 = { color: 'black' };
        } else if (bool === false) {
            $scope.c29 = { color: 'black' };
            $scope.c30 = { color: 'blue' };
        }
    };
    $scope.changeColor30 = function (bool) {

        if (bool == "true") {
            $scope.c30 = { color: 'blue' };
            $scope.c29 = { color: 'black' };
        }
        if (bool == "false" || bool == "") {
            $scope.c30 = { color: 'black' };
            $scope.c29 = { color: 'blue' };
        }
        if (bool === true) {
            $scope.c30 = { color: 'blue' };
            $scope.c29 = { color: 'black' };
        } else if (bool === false) {
            $scope.c30 = { color: 'black' };
            $scope.c29 = { color: 'blue' };
        }
    };
    $scope.changeColor31 = function (bool) {

        if (bool == "true") {
            $scope.c31 = { color: 'blue' };
            $scope.c55 = { color: 'black' };
        }
        if (bool == "false" || bool == "") {
            $scope.c31 = { color: 'black' };
            $scope.c55 = { color: 'blue' };
        }
        if (bool === true) {
            $scope.c31 = { color: 'blue' };
            $scope.c55 = { color: 'black' };
        } else if (bool === false) {
            $scope.c31 = { color: 'black' };
            $scope.c55 = { color: 'blue' };
        }
    };
    $scope.changeColor32 = function (bool) {

        if (bool == "true") {
            $scope.c32 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c32 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c32 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c32 = { color: 'black' };
        }
    };
    $scope.changeColor33 = function (bool) {

        if (bool == "true") {
            $scope.c33 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c33 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c33 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c33 = { color: 'black' };
        }
    };
    $scope.changeColor34 = function (bool) {

        if (bool == "true") {
            $scope.c34 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c34 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c34 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c34 = { color: 'black' };
        }
    };
    $scope.changeColor35 = function (bool) {

        if (bool == "true") {
            $scope.c35 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c35 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c35 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c35 = { color: 'black' };
        }
    };
    $scope.changeColor36 = function (bool) {

        if (bool == "true") {
            $scope.c36 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c36 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c36 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c36 = { color: 'black' };
        }
    };
    $scope.changeColor37 = function (bool) {

        if (bool == "true") {
            $scope.c37 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c37 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c37 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c37 = { color: 'black' };
        }
    };
    $scope.changeColor38 = function (bool) {

        if (bool == "true") {
            $scope.c38 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c38 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c38 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c38 = { color: 'black' };
        }
    };
    $scope.changeColor39 = function (bool) {

        if (bool == "true") {
            $scope.c39 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c39 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c39 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c39 = { color: 'black' };
        }
    };
    $scope.changeColor40 = function (bool) {

        if (bool == "true") {
            $scope.c40 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c40 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c40 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c40 = { color: 'black' };
        }
    };
    $scope.changeColor41 = function (bool) {

        if (bool == "true") {
            $scope.c41 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c41 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c41 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c41 = { color: 'black' };
        }
    };
    $scope.changeColor42 = function (bool) {

        if (bool == "true") {
            $scope.c42 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c42 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c42 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c42 = { color: 'black' };
        }
    };
    $scope.changeColor43 = function (bool) {

        if (bool == "true") {
            $scope.c43 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c43 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c43 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c43 = { color: 'black' };
        }
    };
    $scope.changeColor44 = function (bool) {

        if (bool == "true") {
            $scope.c44 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c44 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c44 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c44 = { color: 'black' };
        }
    };
    $scope.changeColor45 = function (bool) {

        if (bool == "true") {
            $scope.c45 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c45 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c45 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c45 = { color: 'black' };
        }
    };
    $scope.changeColor46 = function (bool) {

        if (bool == "true") {
            $scope.c46 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c46 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c46 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c46 = { color: 'black' };
        }
    };
    $scope.changeColor47 = function (bool) {

        if (bool == "true") {
            $scope.c47 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c47 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c47 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c47 = { color: 'black' };
        }
    };
    $scope.changeColor48 = function (bool) {

        if (bool == "true") {
            $scope.c48 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c48 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c48 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c48 = { color: 'black' };
        }
    };
    $scope.changeColor49 = function (bool) {

        if (bool == "true") {
            $scope.c49 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c49 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c49 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c49 = { color: 'black' };
        }
    };
    $scope.changeColor50 = function (bool) {

        if (bool == "true") {
            $scope.c50 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c50 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c50 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c50 = { color: 'black' };
        }
    };

    $scope.changeColor51 = function (bool) {

        if (bool == "true") {
            $scope.c51 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c51 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c51 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c51 = { color: 'black' };
        }
    };
    $scope.changeColor52 = function (bool) {

        if (bool == "true") {
            $scope.c52 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c52 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c52 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c52 = { color: 'black' };
        }
    };
    $scope.changeColor53 = function (bool) {

        if (bool == "true") {
            $scope.c53 = { color: 'blue' };
        }
        if (bool == "false" || bool == "") {
            $scope.c53 = { color: 'black' };
        }
        if (bool === true) {
            $scope.c53 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c53 = { color: 'black' };
        }
    };

    $scope.Savesms1 = function () {
        debugger;
        var getData = EpisodeService.Savesms($scope.GetPatientDet[0].PID, $scope.smstext, $scope.pntext

            );

        $scope.showerror = false; getData.then(function (pderror) {

            $scope.submiterror = pderror.data;
            //alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.getbalance = function () {
        debugger;
        var getData = EpisodeService.getbalance($scope.clmcat2.mc_catid, $scope.ServiceNo

            );

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.clabal= model.data;
            //$scope.submiterror = pderror.data;
            //alert($scope.submiterror);
           // window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.getbalancetsi = function () {
        debugger;
        var getData = EpisodeService.getbalancetsi($scope.regno

            );

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.clabal = model.data;
            //$scope.submiterror = pderror.data;
            //alert($scope.submiterror);
            // window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Savepatient = function () {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.Savepatient($scope.bitems, $scope.reftohosp, $scope.planofmgt, $scope.drughyst, $scope.hitems, $scope.pastmedhys, $scope.items, $scope.sitems, $scope.litems, $scope.ditems, $scope.Present_Complain, $scope.History_PresentComplain, $scope.Other_Complain, $scope.History_OtherComplain, $scope.OPD_Diagnosis, $scope.AbdOther, $scope.patienthstall[0].PDID, $scope.pntstatus.Status1, $scope.GClinic, $scope.dgn1, $scope.genex, $scope.cardex, $scope.cenex, $scope.resex, $scope.othex, $scope.abdex, $scope.drugins);

        $scope.showerror = false; getData.then(function (pderror) {
            debugger;
            $scope.submiterror = pderror.data;
            $scope.showloader = false; $scope.showerror = true;
            $scope.ditems.splice($scope.ditems);
            $scope.hitems.splice($scope.hitems);
            $scope.litems.splice($scope.litems);
            $scope.items.splice($scope.items);
            $scope.sitems.splice($scope.sitems);
            $scope.bitems.splice($scope.bitems);
           alert($scope.submiterror);
          
           document.location = $("#tp").attr('href');

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Savedemand = function () {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.Savedemand($scope.DName1.itemno, $scope.qnty);

        $scope.showerror = false; getData.then(function (pderror) {
            debugger;
            $scope.submiterror = pderror.data;
            $scope.showloader = false; $scope.showerror = true;

            alert($scope.submiterror);
            window.location.reload();


        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Savestck = function () {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.Savestck($scope.DName1.itemno, $scope.qnty);

        $scope.showerror = false; getData.then(function (pderror) {
            debugger;
            $scope.submiterror = pderror.data;
            $scope.showloader = false; $scope.showerror = true;
            
            alert($scope.submiterror);
            window.location.reload();
           

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     $scope.SavepatientWard = function () {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.SavepatientWard($scope.suitems, $scope.bitems, $scope.reftohosp, $scope.planofmgt, $scope.drughyst, $scope.hitems, $scope.pastmedhys, $scope.items, $scope.sitems, $scope.litems, $scope.ditems, $scope.Present_Complain, $scope.History_PresentComplain, $scope.Other_Complain, $scope.History_OtherComplain, $scope.AbdOther, $scope.patienthstall[0].PDID, $scope.pntStatus.Status1, $scope.GClinic, $scope.dgn1, $scope.genex, $scope.cardex, $scope.cenex, $scope.resex, $scope.othex, $scope.abdex, $scope.drugins, $scope.remarks, $scope.wardNo, $scope.bedNo, $scope.mgtPlan, $scope.OPD_Diagnosis, $scope.PatientCom);

        $scope.showerror = false; getData.then(function (pderror) {
            debugger;
            $scope.submiterror = pderror.data;
            $scope.showloader = false; $scope.showerror = true;
            $scope.ditems.splice($scope.ditems);
            $scope.hitems.splice($scope.hitems);
            $scope.litems.splice($scope.litems);
            $scope.items.splice($scope.items);
            $scope.sitems.splice($scope.sitems);
            $scope.bitems.splice($scope.bitems);
           alert($scope.submiterror);
          
           document.location = $("#tp").attr('href');
           window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

     }

    function GetRelationship() {
        debugger;
        var getData = EpisodeService.GetRelationships();

        $scope.showerror = false; getData.then(function (Relationship) {
            $scope.Relationships = Relationship.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetUserLocation() {
        debugger;
        var getData = EpisodeService.getUserLocation();

        $scope.showerror = false; getData.then(function (location) {
            $scope.LocationID = location.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetRank() {

        var getData = EpisodeService.getRanks();

        $scope.showerror = false; getData.then(function (rank) {
            $scope.ranks = rank.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetTrade() {

        var getData = EpisodeService.getTrade();

        $scope.showerror = false; getData.then(function (trade) {
            $scope.trades = trade.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetDistricts() {

        var getData = EpisodeService.getDistricts();

        $scope.showerror = false; getData.then(function (District) {
            $scope.Districts = District.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetNatureofOccurance(id) {

        var getData = EpisodeService.getNatureofOccurance(id);

        $scope.showerror = false; getData.then(function (rank) {
            $scope.natureofOccurances = rank.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
   
    $scope.PatientHystory = function (id) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystory(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {
               
                $scope.h1 = model.data.h1;
                $mybook = $("#mybook"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
     $scope.PatientHystorypft = function (id) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystorypft(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {
               
                $scope.h1 = model.data.h1;
                $mybook = $("#mybook"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

     }
     $scope.PatientHystoryclaim = function (id) {
         debugger;
         $scope.showloader = true;
         var getData = EpisodeService.PatientHystoryclaim(id);

         $scope.showerror = false; getData.then(function (model) {
             debugger;
             try {

                 $scope.h1 = model.data.h1;
                 $mybook = $("#mybook"),
                 html = $.parseHTML($scope.h1);
                 $mybook.html(html);
                 validate1();
                 $scope.showloader = false; $scope.showerror = true;
             } catch (e) {
                 $scope.showloader = false; $scope.showerror = true;

             }
         }, function () {
             $scope.showloader = false; $scope.showerror = true;
         });

     }
     $scope.PatientHystorygvs = function (id) {
         debugger;
         $scope.showloader = true;
         var getData = EpisodeService.PatientHystorygvs(id);

         $scope.showerror = false; getData.then(function (model) {
             debugger;
             try {

                 $scope.h1 = model.data.h1;
                 $mybook = $("#mybook"),
                 html = $.parseHTML($scope.h1);
                 $mybook.html(html);
                 validate1();
                 $scope.showloader = false; $scope.showerror = true;
             } catch (e) {
                 $scope.showloader = false; $scope.showerror = true;

             }
         }, function () {
             $scope.showloader = false; $scope.showerror = true;
         });

     }
    $scope.PatientHystory1 = function (id) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystory1(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {

                $scope.h1 = model.data.h1;
                $mybook = $("#mybook"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.PatientHystoryWard = function (id,id2) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystoryWard(id, id2);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {

                $scope.h1 = model.data.h1;
                $mybook = $("#wardoop"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.PatientHystoryWardDrugs = function (id, id2) {
        debugger;
        $scope.showloader = true;
        var getData = EpisodeService.PatientHystoryWardDrugs(id, id2);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            try {

                $scope.h1 = model.data.h1;
                $mybook = $("#wardoop"),
                html = $.parseHTML($scope.h1);
                $mybook.html(html);
                validate1();
                $scope.showloader = false; $scope.showerror = true;
            } catch (e) {
                $scope.showloader = false; $scope.showerror = true;

            }
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.GetMainCategory = function (id) {
        debugger;
        var getData = EpisodeService.getMainCategorys(id);

        $scope.showerror = false; getData.then(function (maincategory) {
            $scope.maincategorys = maincategory.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getclaimmob = function (id) {
        
        var getData = EpisodeService.Getclaimmob(id);

        $scope.showerror = false; getData.then(function (mc) {
            debugger;
            $scope.mbno = mc.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.GetSubCategory = function (id) {
        debugger;
        //alert($scope.MainCatCode1.MainCatCode);

        //$scope.subcategorys = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

        var getData = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

        $scope.showerror = false; getData.then(function (subcategory) {
            $scope.subcategorys = subcategory.data;
            $scope.ServicePersonnels = null;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.GetDSDivision = function (id) {
        debugger;

        var getData = EpisodeService.getDSDivision($scope.CL.DIST_CODE);

        $scope.showerror = false; getData.then(function (gsDivition) {
            $scope.gsDivitions = gsDivition.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
        GetTown(id);
        GetPoliceStation(id);
    }

    function GetTown(id) {
        debugger;
        var getData = EpisodeService.getTown($scope.CL.DIST_CODE);

        $scope.showerror = false; getData.then(function (town) {
            $scope.towns = town.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Setlocid = function (id) {
        debugger;


        var getData = EpisodeService.Setlocids($scope.loclisi);

        $scope.showerror = false; getData.then(function (subcategory) {

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    function GetPoliceStation(id) {
        debugger;
        var getData = EpisodeService.getPoliceStation($scope.CL.DIST_CODE);

        $scope.showerror = false; getData.then(function (PoliceStation) {
            $scope.PoliceStations = PoliceStation.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.GetFormation = function (id) {
        debugger;
        //alert($scope.MainCatCode1.MainCatCode);

        //$scope.subcategorys = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

        var getData = EpisodeService.getFormation($scope.ToLocationID.LocationID);

        $scope.showerror = false; getData.then(function (formation) {
            $scope.divisions = formation.data;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.GetServicePersonnel = function (id) {
        debugger;
        $scope.AuthRef = "";
        $scope.ServicePersonnels
        var getData = EpisodeService.getServicePersonnel($scope.ServiceNo);
        $scope.ServicePersonnels = null;
        $scope.showerror = false; getData.then(function (Personnel) {
            $scope.ServicePersonnels = Personnel.data;
        }, function () {
            $scope.alert = "Error in getting records";
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    
    $scope.addregdr = function () {
        debugger;
        for (i = 0; i < $scope.rdrl.length; i++) {
            $scope.daddItem($scope.rdrl[i].ItemNo, $scope.rdrl[i].itemdescription, $scope.rdrl[i].Dose, $scope.rdrl[i].Route, $scope.rdrl[i].RouteDetail, $scope.rdrl[i].Method, $scope.rdrl[i].MethodDetail, $scope.rdrl[i].Duration, $scope.rdrl[i].MethodType, "");
        }

        
       


    }



    $scope.GetDateDiff = function () {
        debugger;
        $scope.Total = "";
        if ($scope.ToDate != "" || $scope.WEFDate != "") {
            var timeDiff = Math.abs($scope.ToDate.getTime() - $scope.WEFDate.getTime());
            var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
            diffDays = diffDays + 1;
            $scope.Total = "(" + diffDays + ")";
        }
    }
    $scope.Getsickloc = function (id) {
        debugger;
        var site = '../Reports/rptSickreport.aspx?opdid=' + id;
        document.getElementById('iFrameName').src = site;
    }
    $scope.GetDivAP1 = function (id) {
        if (id == 1) {
            $scope.divAP1new = true;
            $scope.divAP1cease = false;
        }
        else {
            $scope.divAP1cease = true;
            $scope.divAP1new = false;
        }
    }
    $scope.loaddorder = function (id) {
        debugger;
        // $scope.ditems = "";
        var getData = EpisodeService.loaddorder(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.loadlab = model.data;

        }, function () {
            $scope.showerror = true;
        });
    }

    $scope.editRole = function (role) {
        debugger;
        var getData = EpisodeService.getRole(role.Id);
        $scope.showerror = false; getData.then(function (rol) {
            $scope.employee = rol.data;
            $scope.employeeId = role.Id;
            $scope.employeeName = role.name;
            $scope.employeeEmail = role.email;
            $scope.employeeAge = role.Age;
            $scope.Action = "Update";
            $scope.divRole = true;
        },
       function () {
           $scope.showloader = false; $scope.showerror = true;
       });
    }

    $scope.AddUpdateRole = function () {
        debugger;

        var Role = {
            PDID: $scope.ServiceNo,
            PID: $scope.ServiceNo,
            Present_Complain: $scope.Present_Complain,
            OPD_ID: '1',
            Status: '1',
            CreatedUser: '1',
            CreatedDate: '20 jan 2011'
        };

        var getAction = $scope.Action;

        if (getAction == "Update") {
            Role.Id = $scope.PID;
            var getData = EpisodeService.updateRole(Role);
            $scope.showerror = false; getData.then(function (msg) {
                //GetAllRole();
                $scope.alert = msg.data;
                $scope.divRole = false;
            }, function () {
                $scope.alert = "Error in adding record";
                //alert('Error in updating record');
            });
        } else {

            var getData = EpisodeService.AddEpisode(Role);
            $scope.showerror = false; getData.then(function (msg) {
                //getAction.alert();
                //$scope.alert = msg.data;
                //alert(msg.data);
                $scope.successTextAlert = "Saved " + $scope.ServiceNo;
                $scope.showSuccessAlert = true;
            });
            // switch flag
            $scope.switchBool = function (value) {
                $scope[value] = !$scope[value];
            };

        }

    }

    $scope.AddRoleDiv = function () {
        debugger;
        ClearFields();
        $scope.Action = "Add";
        $scope.divList = false;
        $scope.divPor = true;
    }

    $scope.ViewRole = function () {
        debugger;
        ClearFields();
        $scope.Action = "View";
        $scope.divRole = false;
        $scope.divList = true;
    }

    $scope.deletePorDtls = function (id) {
        debugger;
        var getData = EpisodeService.DeletePorDtls($scope.LocationID + "-" + $scope.PorHeader1 + "-" + $scope.Year + "-" + $scope.PorNo + "-" + id);
        $scope.showerror = false; getData.then(function (msg) {
            //GetAllPOR($scope.PorNo);
            //GetAllRole($scope.LocationID + "-" + $scope.PorHeader1 + "-" + $scope.Year + "-" + $scope.PorNo);
            //alert(msg.data);
            ////ClearFields();

            $scope.Action = "View";
            ClearDiv();
            $scope.divPORlist = true;
            $scope.PorHeader1.PorCat = $scope.PorHeader1.trim();

            id = $scope.LocationID + "-" + $scope.PorHeader1 + "-" + $scope.Year + "-" + $scope.PorNo;
            //GetPorDtls(id);
            var getData = EpisodeService.getPors(id);

            $scope.showerror = false; getData.then(function (por) {
                $scope.pors = por.data;
            }, function () {
                $scope.showloader = false; $scope.showerror = true;
            });


        }, function () {

            //alert('Error in Deleting Record');
        });

        //Load POR details Grid


    }

    function ClearFields() {
        $scope.ServiceNo = "";
        $scope.OccNo = $scope.OccNo + 1;
        $scope.WEFDate = "";
        $scope.AuthRef = "";
        $scope.name = "";
        $scope.natureofOccurances = "";
        $scope.FromLocationID = "";
        $scope.ToLocationID = "";
        $scope.ToDate = "";
        $scope.attachmentType = "";
        $scope.hospital = "";
        $scope.LL = "";
        $scope.address = "";
        $scope.PH = "";
        $scope.PL = "";
        $scope.AL = "";
        $scope.attachmentType = "";
        $scope.RDate = "";
        $scope.MaritialStatus = "";
        $scope.LivingStatus = "";
        $scope.ServicePersonnels = null;
    }

    function ClearDiv() {
        $scope.divPORlist = false;
        $scope.divCancellation = false;
        $scope.divPromotion = false;
        $scope.divEnlistment = false;
        $scope.divAA2 = false;
        $scope.divAA5 = false;
        $scope.divAA6 = false;
        $scope.divAB1 = false;
        $scope.divAC2 = false;
        $scope.divAC3 = false;
        $scope.divAC4 = false;
        $scope.divAC5 = false;
        $scope.divAC6 = false;
        $scope.divAC7 = false;
        $scope.divAC8 = false;
        $scope.divAD1 = false;
        $scope.divAE1 = false;
        $scope.divAE2 = false;
        $scope.divAE3 = false;
        $scope.divAE5 = false;
        $scope.divAE6 = false;
        $scope.divAE7 = false;
        $scope.divAF2 = false;
        $scope.divAG2 = false;
        $scope.divAG4 = false;
        $scope.divAG5 = false;
        $scope.divPromotion = false;
        $scope.divAH3 = false;
        $scope.divAJ1 = false;
        $scope.divAJ2 = false;
        $scope.divAJ3 = false;
        $scope.divAK1 = false;
        $scope.divAK2 = false;
        $scope.divAK3 = false;
        $scope.divAK4 = false;
        $scope.divAL6 = false;
        $scope.divAM1 = false;
        $scope.divAM3 = false;
        $scope.divAM4 = false;
        $scope.divAM5 = false;
        $scope.divAM9 = false;
        $scope.divAN1 = false;
        $scope.divAO1 = false;
        $scope.divAO2 = false;
        $scope.divAO3 = false;
        $scope.divAP1 = false;
        $scope.divAQ1 = false;
        $scope.divAQ2 = false;
        $scope.divAQ5 = false;
        $scope.divAR1 = false;
        $scope.divAR2 = false;
        $scope.divAS1 = false;
        $scope.divAS2 = false;
        $scope.divAT1 = false;
        $scope.divAT2 = false;
        $scope.divAT3 = false;
        $scope.divAU1 = false;
        $scope.divAA2 = false;
        $scope.divAA5 = false;
        $scope.divAA6 = false;
        $scope.divAB1 = false;
    }

    function GetNextPORNo() {
        var getData = EpisodeService.getNextPORNo($scope.LocationID, $scope.PorHeader1.PorCat, $scope.Year);

        $scope.showerror = false; getData.then(function (Personnel) {

            $scope.ServicePersonnels = Personnel.data;
            //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
            //$scope.rankname = Personnel.RNK_NAME;
            //$scope.surname = Personnel.ActiveNo;

        }, function () {
            $scope.alert = "Error in getting records";
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    function GetMaxOccuranceNo(id) {
        debugger;
        var getData = EpisodeService.getMaxOccuranceNo(id);

        $scope.showerror = false; getData.then(function (por) {

            $scope.OccNo = por.data + 1;
            //$scope.OccNo = $scope.por;
            //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
            //$scope.rankname = Personnel.RNK_NAME;
            //$scope.surname = Personnel.ActiveNo;

        }, function () {
            $scope.alert = "Error in getting records";
            $scope.showloader = false; $scope.showerror = true;
        });

    }



    $scope.AA2EXTENSIONOFSERVICE = function (ExtendedBy, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ExtendedBy == undefined || ExtendedBy == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {

            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }

    }

    $scope.AA5CONTINUANCEOFSERVICE = function (ExtendedBy, AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ExtendedBy == undefined || ExtendedBy == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {
        }
        else {

            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }
    $scope.printpage = function () {
        debugger;
        var headstr = "<html><head><title></title><style>#labtb{font-size:12px;}</style></head><body>";
        var footstr = "</body>";
        //  var newstr = document.all.item(divName).innerHTML;
       // var cnt = document.getElementById("pgnm").innerText;
        var newstr = document.getElementById("a" +$scope.pgnm).innerHTML;
        var oldstr = document.body.innerHTML;
        var mywin = window.open('', 'PRINT', 'height=600,width=1200');
        mywin.document.write('<html><head><title></title><style>#labtb{font-size:12px;}</style></head><body>');
        mywin.document.write(newstr);
        mywin.document.write('</body>');
        mywin.document.close();
        mywin.focus();
        mywin.print();
        mywin.close();
        // document.body.innerHTML = headstr + newstr + footstr;
      //  window.print();
       // window.location.reload();
        // document.body.innerHTML = oldstr;
        return false;
        // window.print();
    }
    $scope.printDiv = function (divName) {
        debugger;
        var headstr = "<html><head><title><style>#labtb{font-size:12px;}</style></title></head><body><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>";
        var footstr = "</body>";
        var newstr = document.all.item(divName).innerHTML;
        var oldstr = document.all.item('svcpr').innerHTML;
       
        var mywin = window.open('', 'PRINT', 'height=600,width=1200');
        mywin.document.write(headstr + oldstr + '<br/>' + Date() + '<br/>' + newstr + footstr);
       
        mywin.document.close();
        mywin.focus();
        mywin.print();
        mywin.close();
        return false;
        // window.print();
    }
    $scope.AA6REENGAGEMENT = function () { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (true) {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }
        else {
            //alert("Error");
        }
    }

    $scope.AB1REINSTATEMENT = function () { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (true) {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }
        else {
            //alert("Error");
        }
    }

    $scope.AC2CHANGEOFNAME = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef != "") {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }
        else {
            //alert("Error");
        }
    }

    $scope.AC3PERMENEMTADDRESS = function () { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (true) {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }
        else {
            //alert("Error");
        }
    }

    $scope.AA6REENGAGEMENT = function (ExtendedBy, AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ExtendedBy == undefined || ExtendedBy == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {
        }
        else {

            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }


        }
    }

    $scope.AB1REINSTATEMENT = function (ExtendedBy, AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ExtendedBy == undefined || ExtendedBy == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {
        }
        else {

            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";


            }

        }
    }

    $scope.AC2CHANGEOFNAME = function (name, WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (name == undefined || name == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {
        }
        else {

            if ($scope.ServiceNo != "") {

                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AC3PERMENEMTADDRESS = function (address, WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (address == undefined || address == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {
        }
        else {

            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }


        }
    }

    $scope.AC4ADDITIONALNOK = function (AuthRef) { //MLJ
        debugger;
        if (AuthRef == undefined || AuthRef == "") {
        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }
    }

    $scope.AC5MOBILETELEPHONENO = function (address, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {
        }
        else if (address == "" || address == undefined) {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }
    }

    $scope.AC6RESIDENTIALTELEPHONENO = function (address, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {
        }
        else if (address == "" || address == undefined) {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }
    }

    $scope.AC7EEMAILADDRESS = function (address) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (address == undefined || address == "") {

        }
        else {
            if (/^\w+([\.-]?\w+)@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(address)) {
                if ($scope.ServiceNo != "") {
                    $scope.ckbtnsubmit = "true";
                    // CheckPerson($scope.ServiceNo);

                }
            }
        }

    }

    $scope.AC8DEATHOFCHILD = function (ToDate, address, WEFDate, AuthRef) { //MLJ
        debugger;
        //  var now = Date.now; // Date.Now();
        var now = new Date();
        $scope.ckbtnsubmit = "";
        if (ToDate == undefined || ToDate == "") {

        }
        else if (address == undefined || address == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if (WEFDate < now) {
                if ($scope.ServiceNo != "") {
                    $scope.ckbtnsubmit = "true";

                }
            }
        }

    }

    $scope.AE1AE2AE3AE5BASICCOMBATCOURSE = function (AuthRef, ToDate, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ToDate == undefined || ToDate == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }

    }

    $scope.AE6AE7LOCALCOURSEWORKSHOPSEMINARS = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AF2RECLASSIFICATIONLTTBCTTB = function (marks, WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (marks == undefined || marks == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AG2AG3HOSPITALIZATION = function (AuthRef, ToDate, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (ToDate == undefined || ToDate == "") {

        }
        else if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AG4MEDICALCATEGORYTEMPORARY = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AG5MEDICALCATEGORYPERMANENT = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AJ1MARRIAGE = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AJ2DIVORCE = function (OccNo, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (OccNo == undefined || OccNo == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }
    }

    $scope.AJ3WIDOW = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AK1LIVINGINOUT = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AK2OCCUPATIONOFAMQ = function (name, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (name == undefined || name == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AK3VACATIONOFAMQ = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AK4NOK = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AL6GOODCONDUCTEDBADGES = function (name, AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (name == undefined || name == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AM1AM2SUMMERYTRIALPUNISHMENTS = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AM3RECOVERIES = function (spouse, WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else if (spouse == undefined || spouse == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AM4REDUCTIONS = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }
        }

    }

    $scope.AM5AM6AM7AM8LETTEROFWARNING = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AM9CIVILCOURTCASES = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AH1PROMOTIONS = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";

            }

        }
    }

    $scope.AH2APPOINTMENT = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AH3MUSTERINGS = function () { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (true) {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }

    }

    $scope.AN1LEAVE = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AO1MOVEMENTTEMPORARYDUTIES = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AO2MOVEMENTATTACHMENT = function () { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (true) {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }
        }
        else {
            //alert("Error");
        }
    }

    $scope.AO2MOVEMENTATTACHMENTDate = function (frD, toD) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (frD == undefined || frD == "") {

        }
        else if (toD == undefined || toD == "") {

        }
        else {
            $scope.ckbtnsubmit = "true";
        }
    }

    $scope.AO3MOVEMENTPOSTING = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AP1ALLOWANCES = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AQ1AWOL = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AQ2AQ3AQ4KIA = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AQ5DEATH = function (AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AR1SUSPENSIONS = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AR2DEMOBILIZATION = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AS1FORFEITUREOFSERVICE = function (AuthRef, WEFDate) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AS2RESTORATIONS = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AT1SECONDARYDUTIES = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AT2SPORT = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AT3SPECIALACHIEVEMENT = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }

    $scope.AU1AU2AU3AU4AU5DISCHARGE = function (WEFDate, AuthRef) { //MLJ
        debugger;
        $scope.ckbtnsubmit = "";
        if (WEFDate == undefined || WEFDate == "") {

        }
        else if (AuthRef == undefined || AuthRef == "") {

        }
        else {
            if ($scope.ServiceNo != "") {
                $scope.ckbtnsubmit = "true";
            }

        }
    }









    //function CheckPerson(SvcNo) { //MLJ
    //    debugger;
    //    if (ServiceNo == undefined || ServiceNo == "") {

    //    }
    //    else {
    //        var getData = EpisodeService.getServicePersonnel(SvcNo, $scope.PorHeader1);
    //        $scope.ServicePersonnels = null;
    //         $scope.showerror =false; getData.then(function (Personnel) {
    //            $scope.ServicePersonnels = Personnel.data;
    //        }, function () {
    //        });

    //        var authority = {
    //            ServiceNo: $scope.ServiceNo,
    //            SubCatCode: $scope.SubCatCode.SubCatCode,
    //            SNo: $scope.ServiceNo,
    //            P3Authority: " "
    //        }

    //        if ($scope.ServicePersonnels[0].RNK_NAME != "") {
    //            $scope.ckbtnsubmit = "true";
    //        }

    //    }
    //}

});