app.controller("LabCntrl", function ($scope, LabService) {
    $scope.divPor = true;
    $scope.startsWith = function (ItemDescription, viewValue) {
        debugger;
        return ItemDescription.substr(0, viewValue.length).toLowerCase() == viewValue.toLowerCase();
    }

    $scope.Controls = [];
    $scope.master = {};

    ////////////////
 
     $scope.initdrug = function () {
         $scope.showloader = true;
         debugger;
         $scope.DName = JSON.parse(localStorage.getItem("dlist1"));
       
         if ($scope.DName == '' || $scope.DName == undefined) {
         
            
             GetDrug();
             Getperdose();
             GetDrugtime();
             GetRoute();
             GetMethod();
         }
         else {
             Getperdose();
             GetDrugtime();
             GetRoute();
             GetMethod();
             debugger;
             $scope.states = $scope.DName.itemdescription;
         }
    }
     $scope.isNumber = function (n) {
        // debugger;
         return !isNaN(parseFloat(n)) && isFinite(n);
         
     }

    $scope.psindex1 = "";
    $scope.issudqnty1 = "";
    $scope.psitems = [];
    $scope.adddrugItem = function (Ps_Index, issudqnty) {
        debugger;

        for (var i = 0; i < $scope.psitems.length; i++) {
            if ($scope.psitems[i].psindex1 == Ps_Index) {
                $scope.psitems.splice(i, 1);
            }
        }
        $scope.psitems.push({
           
            psindex1: Ps_Index,
            issudqnty1: issudqnty

        });
       
        $scope.Ps_Index = "";
        $scope.issudqnty = "";
    }

    /////////////



   
    $scope.SubCategoryName1 = "";
    $scope.Lab_Index1 = "";
     $scope.Labst1 = "";
    $scope.items = [];
    $scope.addItem = function (SubCategoryName, Lab_Index,labst) {
        debugger;

        
        if (SubCategoryName) {
          
            //var exist=false;
            //for(var i=0;i<$scope.items.length;i++){
            //    if ($scope.items[i].Lab_Index1 == Lab_Index) {
            //        exist=true;
            //    }
            //}
            for (var i = 0; i < $scope.items.length; i++) {
                if ($scope.items[i].Lab_Index1 == Lab_Index) {
                    $scope.items.splice(i, 1);
                }
            }


         //   if (!exist) {
                $scope.items.push({
                    Labst1: labst,
                    SubCategoryName1: SubCategoryName,
                    Lab_Index1: Lab_Index

                });
           // }
        }
        $scope.labst = "";
        $scope.LabTestID = "";
        $scope.Lab_Index = "";
    }
  
    $scope.hremoveItem = function (index) {
        debugger;
        $scope.items.splice(index, 1);
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
    $scope.disq1 = "";
    $scope.ditems = [];
    $scope.daddItem = function (Itemno, Itemno1, Dose, Route, Route1, Method, Method1, Duration, StockTypeID,Isuq) {
        debugger;
        for (var i = 0; i < $scope.ditems.length; i++) {
            if ($scope.ditems[i].dItemno == Itemno) {
                $scope.ditems.splice(i, 1);
            }
        }
        if (Duration == '' || Duration == undefined) {
            $("#errormsgspan").text("Please Add no of Days");
            //return false;
        } else if (Method1 == undefined) {
            $("#errormsgspan").text("Please Select Method");
            //return false;
        
        
        } else if (Route1 == undefined) {
        $("#errormsgspan").text("Please Select Route");
        //return false;
        
    }
        else if (Isuq == '' || Isuq == undefined) {
        $("#errormsgspan").text("Please Add Quantity");
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
                disq1: Isuq,
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

            $scope.Route = "";
            $scope.Route1 = "";
            $scope.Method = "";
            $scope.Method1 = "";
            $scope.Duration = "";
            $scope.StockTypeID = "";
            $scope.Isuq = "";
        }
    }

    $scope.dremoveItem = function (index) {
        debugger;
        $scope.ditems.splice(index, 1);
    }


    $scope.scatid = "";
    $scope.scategory = "";
    $scope.sdays = "";
   
    $scope.sitems = [];
    $scope.saddItem = function (scatid1, scategory1, sdays1) {
        debugger;

        $scope.sitems.push({
            scatid: scatid1,
            scategory: scategory1,
            sdays: sdays1
        });
        $scope.scatid1 = "";
        $scope.scategory1 = "";
        $scope.sdays1 = "";
    }

    $scope.sremoveItem = function (index) {
        debugger;
        $scope.sitems.splice(index, 1);
    }
    $scope.printDiv = function (divName) {
        debugger;
        var headstr = "<html><head><title></title><style>#labtb{font-size:12px;}</style></head><body>";
        var footstr = "</body>";
        var newstr = document.all.item(divName).innerHTML;
        var oldstr = document.body.innerHTML;
        document.body.innerHTML = headstr + newstr + footstr;
        window.print();
        window.location.reload();
       // document.body.innerHTML = oldstr;
        return false;
       // window.print();
    }
    $scope.submitvitals = function (ite) {
        debugger;
        var getData = EpisodeService.submitvital(ite);

         $scope.showerror =false;  getData.then(function (hms) {


        }, function () {
             $scope.showerror =true; 
        });

    }

   
    //$scope.loadchild = function (id) {
    //    debugger;
    //    $scope.GetPatientDet[0] = id;
      
        
    //    var getData = EpisodeService.loadchildimg(id.PID);
    //     $scope.showerror =false;  getData.then(function (model) {
    //        debugger;
          
           

    //        $scope.primage = model.data;

    //    }, function () {
    //         $scope.showerror =true; 
    //    });
       
    //}
    $scope.acctest = function (Testsid, tubcat) {
        debugger;
        $scope.showloader = true;
        var getData = LabService.acctest(Testsid, tubcat);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.viewlab = model.data;
            $scope.showloader = false;
            alert("Saved!");
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });
    }

    function GetDrugtime() {
        debugger;
        var getData = LabService.GetDrugtime();

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
        var getData = LabService.Getperdose();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.Dose3 = mod.data;

            $scope.Dose = $scope.Dose3[0];
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    
        $scope.Getspins = function (id) {
        debugger;
        var getData = LabService.Getspins(id);

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.spinsdr = mod.data.s1[0].Examination;
            $scope.phyp = mod.data.l1;
         
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Getserviceno = function (id) {
        debugger;
        $scope.rankselect
        $scope.sexselect
        $scope.ServicePersonnels
        $scope.dtofb
        var getData = EpisodeService.getsp(id);

        $scope.ServicePersonnels = null;
        $scope.rankselect = null;
        $scope.dtofb = new Date;
         $scope.showerror =false;  getData.then(function (hms) {
            debugger;
            //$scope.Surname = hms.data[0].Surname;
            $scope.ServicePersonnels = hms.data;
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
            ////alert($scope.ServicePersonnels[0].Surname);
            //$scope.rankname = $scope.ServicePersonnels.RNK_NAME;
            //$scope.rankname = Personnel.RNK_NAME;
            //$scope.surname = Personnel.ActiveNo;
            // $scope.WefDate = $scope.por[0].WefDate;

        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.Getrnks = function () {

        var getData = EpisodeService.getrnks();

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.rnks = model.data;


        }, function () {
             $scope.showerror =true; 
        });
    }
    $scope.getsex = function () {

        var getData = EpisodeService.getsex();

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.sxs = model.data;

        }, function () {
             $scope.showerror =true; 
        });
    }


    $scope.getrelations = function () {
        $scope.getrelet

        var getData = EpisodeService.getrelation();
        $scope.getrelet = null;

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.getrelet = model.data;
            $scope.relet = $scope.getrelet[0];
        }, function () {
             $scope.showerror =true; 
        });
    }
    //To Get All Records 
   

  
    $scope.Searchlabsno = function () {
        debugger;
        var getData = LabService.Searchlabsno(1,$scope.SearchString);

         $scope.showerror =false;  getData.then(function (por) {

            $scope.POR = por.data;
           

        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetVitalType() {
        debugger;
        var getData = EpisodeService.GetVitalTypes();

         $scope.showerror =false;  getData.then(function (VitalType) {
            $scope.VitalTypes = VitalType.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetSeverityTypes() {
        debugger;
        var getData = EpisodeService.GetSeverityTypes();

         $scope.showerror =false;  getData.then(function (SeverityTypes) {
            $scope.SeverityType = SeverityTypes.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetRoute() {
        debugger;
        var getData = LabService.GetRoute();

         $scope.showerror =false;  getData.then(function (mod) {
            $scope.Route3 = mod.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetMethod() {
        debugger;
        var getData = LabService.GetMethod();

         $scope.showerror =false;  getData.then(function (mod1) {
             $scope.Method3 = mod1.data;
             $scope.showloader = false;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetDrug() {
        debugger;
        var getData = LabService.GetDrug();

        $scope.showerror = false; getData.then(function (mod) {
             debugger;
             $scope.DName = mod.data;
             localStorage.setItem("dlist","yes");
             localStorage.setItem("dlist1", JSON.stringify($scope.DName));

             $scope.states = $scope.DName.itemdescription;
             $scope.showloader = false;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetSicktype() {
        debugger;
        var getData = EpisodeService.GetSicktype();

         $scope.showerror =false;  getData.then(function (mod) {
            $scope.CName = mod.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetHyperMainType() {
        debugger;
        var getData = EpisodeService.GetHyperMainType();

         $scope.showerror =false;  getData.then(function (HyperTypes) {
            $scope.HyperType = HyperTypes.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetHyperReactType() {
        debugger;
        var getData = EpisodeService.GetHyperReactType();

         $scope.showerror =false;  getData.then(function (HyperReact) {
            $scope.HypRMainCategory = HyperReact.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    $scope.GetHyperSubType = function (id) {
        debugger;
      

        var getData = EpisodeService.GetHyperSubType1($scope.HyperType1.HypersenceMainCatID);

         $scope.showerror =false;  getData.then(function (subcategory) {
            $scope.HyperSubType = subcategory.data;
          
        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.GetReactSubType = function (id) {
        debugger;


        var getData = EpisodeService.GetReactSubType($scope.HypRMainCategory1.HypRMainID);

         $scope.showerror =false;  getData.then(function (subcategory) {
            $scope.HypRSubCategory = subcategory.data;

        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.GetPatient = function (id, relet) {

        $scope.GetPatientDet
        $scope.GetPatientDetdr
        $scope.primage

        var getData = EpisodeService.GetPatients(id, relet);
        $scope.GetPatientDet = null;
        $scope.GetPatientDetdr = null;
        $scope.primage = null;
         $scope.showerror =false;  getData.then(function (hms) {
            $scope.GetPatientDet = hms.data.serv;
            $scope.PatientDetdr = hms.data.serv;
           // $scope.pidnew = $scope.PatientDetdr[0];
            $scope.primage = hms.data.imgd;

        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.toggle = function () {
        debugger;
        $scope.myVar = !$scope.myVar;
    };


    $scope.table = { fields: [] };

    $scope.addFormField = function () {
        $scope.table.fields.push('');
    }

    $scope.submitTable = function () {
        console.log($scope.table);
    }



    $scope.loadlabtest = function (id,catid) {
        debugger;
       // $scope.ditems = "";
        var getData = LabService.loadlabtest(id, catid);

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.loadlab = model.data;
         
        }, function () {
             $scope.showerror =true; 
        });
    }
    $scope.loaddruglist = function (id) {
        debugger;
        document.getElementById("svd").disabled = false;
        $("#svd").addClass('btn btn-default');
      //  $scope.ditems = "";
        var getData = LabService.loaddruglist(id);

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.loaddrug = model.data;
            if (model.data=="") {
                alert("No Data!");
            }
        }, function () {
             $scope.showerror =true; 
        });
    }
    $scope.loadisudruglist = function (id) {
        debugger;
       // $scope.ditems = "";
        var getData = LabService.loadisudruglist(id);

        $scope.showerror = false; getData.then(function (model) {
            debugger;
            $scope.loaddrug = model.data;

        }, function () {
            $scope.showerror = true;
        });
    }
    $scope.viewlabtest = function (id, catid) {
        debugger;
        // $scope.ditems = "";
        $scope.showloader = true;
        var getData = LabService.viewlabtest(id, catid);

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.viewlab = model.data;
            $scope.showloader = false;
        }, function () {
             $scope.showerror =true; 
        });
    }
    $scope.loadcamp = function () {
        debugger;
      //  $scope.ditems = "";
        var getData = LabService.loadcamp();

         $scope.showerror =false;  getData.then(function (model) {
            debugger;
            $scope.viewcamp = model.data;

        }, function () {
             $scope.showerror =true; 
        });
    }
    $scope.Submitpatient = function () { 
        debugger;
        var getData = EpisodeService.Submitpatient($scope.items, $scope.hitems, $scope.Present_Complain, $scope.GetPatientDet[0].PID, $scope.HDetail);

         $scope.showerror =false;  getData.then(function (pderror) {
            $scope.submiterror = pderror.data;
            
            //alert($scope.submiterror);
        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.changeColor1 = function (bool) {
        
        if (bool == "true") {
            $scope.c1 = { color: 'blue' };
        }
        if (bool == "false"||bool =="") {
                $scope.c1 = { color: 'black' };
            }
            if (bool === true) {
                $scope.c1 = { color: 'blue' };
        } else if (bool === false) {
            $scope.c1 = { color: 'black' };
        }
    };
    
    $scope.saveditem = function () {
        debugger;
        var getData = EpisodeService.saveditem($scope.DName1.itemdescription, $scope.qnty);

        $scope.showerror = false; getData.then(function (pderror) {

            $scope.submiterror = pderror.data;
            //alert($scope.submiterror);
            window.location.reload();
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    
    $scope.Getepasdrug = function (id) {
        $scope.showloader = true;
        $scope.epasdr
        var getData = LabService.Getepasdrug(id);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasdr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }

    $scope.Getepasrpcdrug = function (id) {
        $scope.showloader = true;
        $scope.epasdr
        var getData = LabService.Getepasrpcdrug(id);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.showloader = false;
            $scope.epasdr = sick.data;

        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }


    $scope.initdrug1 = function () {
        debugger;

        Getrpcdrug();
    }
    function Getrpcdrug() {
        debugger;
        var getData = LabService.Getrpcdrug();

        $scope.showerror = false; getData.then(function (mod) {
            debugger;
            $scope.rpcdr = mod.data;
            $scope.states = $scope.rpcdr.ItemDescription;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.savepasdrug = function (id) {
        $scope.showloader = true;
        $scope.epasdr
        var getData = LabService.savepasdrug(id);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.epasdr1 = sick.data;
            $scope.showloader = false;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
   
    $scope.savepasrpcdrug = function (id) {
        $scope.showloader = true;
        $scope.epasdr
        var getData = LabService.savepasrpcdrug(id);
        $scope.epasdr = null;
        $scope.showerror = false; getData.then(function (sick) {
            debugger;
            $scope.epasdr1 = sick.data;
            $scope.showloader = false;
        }, function () {
            $scope.showloader = false; $scope.showerror = true;
        });

    }
    $scope.Savereport = function () {
        debugger;
        var getData = LabService.Savereport($scope.items);

         $scope.showerror =false;  getData.then(function (pderror) {
            window.location.reload();
            $scope.submiterror = pderror.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    $scope.Drugissued = function (PDID) {

        debugger;
        document.getElementById("svd").disabled = true;
        $("#svd").removeAttr('class');
        var getData = LabService.Drugissued($scope.loaddrug[0].PDID, $scope.psitems, $scope.ditems);

         $scope.showerror =false;  getData.then(function (pderror) {
            window.location.reload();
            $scope.submiterror = pderror.data;
        }, function () {
             $scope.showerror =true; 
        });

    }
    function GetRelationship() {
        debugger;
        var getData = EpisodeService.GetRelationships();

         $scope.showerror =false;  getData.then(function (Relationship) {
            $scope.Relationships = Relationship.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetUserLocation() {
        debugger;
        var getData = EpisodeService.getUserLocation();

         $scope.showerror =false;  getData.then(function (location) {
            $scope.LocationID = location.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetRank() {

        var getData = EpisodeService.getRanks();

         $scope.showerror =false;  getData.then(function (rank) {
            $scope.ranks = rank.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetTrade() {

        var getData = EpisodeService.getTrade();

         $scope.showerror =false;  getData.then(function (trade) {
            $scope.trades = trade.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetDistricts() {

        var getData = EpisodeService.getDistricts();

         $scope.showerror =false;  getData.then(function (District) {
            $scope.Districts = District.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

  

    $scope.GetMainCategory = function (id) {
        debugger;
        var getData = EpisodeService.getMainCategorys(id);

         $scope.showerror =false;  getData.then(function (maincategory) {
            $scope.maincategorys = maincategory.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    $scope.GetSubCategory = function (id) {
        debugger;
        ////alert($scope.MainCatCode1.MainCatCode);

        //$scope.subcategorys = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

        var getData = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

         $scope.showerror =false;  getData.then(function (subcategory) {
            $scope.subcategorys = subcategory.data;
            $scope.ServicePersonnels = null;
        }, function () {
             $scope.showerror =true; 
        });

    }

    $scope.GetDSDivision = function (id) {
        debugger;

        var getData = EpisodeService.getDSDivision($scope.CL.DIST_CODE);

         $scope.showerror =false;  getData.then(function (gsDivition) {
            $scope.gsDivitions = gsDivition.data;
        }, function () {
             $scope.showerror =true; 
        });
        GetTown(id);
        GetPoliceStation(id);
    }

    function GetTown(id) {
        debugger;
        var getData = EpisodeService.getTown($scope.CL.DIST_CODE);

         $scope.showerror =false;  getData.then(function (town) {
            $scope.towns = town.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    function GetPoliceStation(id) {
        debugger;
        var getData = EpisodeService.getPoliceStation($scope.CL.DIST_CODE);

         $scope.showerror =false;  getData.then(function (PoliceStation) {
            $scope.PoliceStations = PoliceStation.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    $scope.GetFormation = function (id) {
        debugger;
        ////alert($scope.MainCatCode1.MainCatCode);

        //$scope.subcategorys = EpisodeService.getSubCategorys($scope.MainCatCode1.MainCatCode);

        var getData = EpisodeService.getFormation($scope.ToLocationID.LocationID);

         $scope.showerror =false;  getData.then(function (formation) {
            $scope.divisions = formation.data;
        }, function () {
             $scope.showerror =true; 
        });

    }

    $scope.GetServicePersonnel = function (id) {
        debugger;
        $scope.AuthRef = "";
        $scope.ServicePersonnels
        var getData = EpisodeService.getServicePersonnel($scope.ServiceNo);
        $scope.ServicePersonnels = null;
         $scope.showerror =false;  getData.then(function (Personnel) {
            $scope.ServicePersonnels = Personnel.data;
        }, function () {
            $scope.//alert = "Error in getting records";
             $scope.showerror =true; 
        });

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


    $scope.editRole = function (role) {
        debugger;
        var getData = EpisodeService.getRole(role.Id);
         $scope.showerror =false;  getData.then(function (rol) {
            $scope.employee = rol.data;
            $scope.employeeId = role.Id;
            $scope.employeeName = role.name;
            $scope.employeeEmail = role.email;
            $scope.employeeAge = role.Age;
            $scope.Action = "Update";
            $scope.divRole = true;
        },
        function () {
             $scope.showerror =true; 
        });
    }

    $scope.AddUpdateRole = function () {
        debugger;

        var Role = {
            PDID: $scope.ServiceNo,
            PID: $scope.ServiceNo,
            Present_Complain: $scope.Present_Complain,
            OPD_ID:'1',
            Status: '1',
            CreatedUser: '1',
            CreatedDate: '20 jan 2011'
        };

        var getAction = $scope.Action;

        if (getAction == "Update") {
            Role.Id = $scope.PID;
            var getData = EpisodeService.updateRole(Role);
             $scope.showerror =false;  getData.then(function (msg) {
                //GetAllRole();
                $scope.alert = msg.data;
                $scope.divRole = false;
            }, function () {
                $scope.alert = "Error in adding record";
                //alert('Error in updating record');
            });
        } else {

            var getData = EpisodeService.AddEpisode(Role);
             $scope.showerror =false;  getData.then(function (msg) {
                //getAction.//alert();
                //$scope.//alert = msg.data;
                //alert(msg.data);
                $scope.successText//alert = "Saved " + $scope.ServiceNo;
                $scope.showSuccess//alert = true;
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
         $scope.showerror =false;  getData.then(function (msg) {
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

             $scope.showerror =false;  getData.then(function (por) {
                $scope.pors = por.data;
            }, function () {
                 $scope.showerror =true; 
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

   

 

});