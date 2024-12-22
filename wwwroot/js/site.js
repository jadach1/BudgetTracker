//Format 2024-5-2 to Thursday May 2024
var listOfMonths = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var listOfDays = ["Sunday","Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
var dictionaryOfMonth = new Map([["january",1], ["february",2],["march",3],["april",4],["may",5],["june",6],["july",7],["august",8],["september",9],["october",10],["november",11],["december",12],])


function humanReadableDate(date){    
    const d = new Date(date);
    let month = listOfMonths[d.getMonth()];
    let day = listOfDays[d.getDay()];
    let year = d.getFullYear();
    return day+" "+month+" "+year;
}

//Returns the height and width of the screen
function getCurrentScreenSize(){
 const $Screen = $("body");
 const windowHeight = window.innerHeight;
 const windowWidth = window.innerWidth;
    return {
            width: $Screen.width(),
            height: $Screen.height(),
            windowHeight: windowHeight,
            windowWidth: windowWidth
        }
}

function screenSizeCheck(){
    const size = +document.getElementsByTagName("body")[0].clientWidth;
    const height = +document.getElementsByTagName("body")[0].clientHeight;
    const bbody = document.getElementsByTagName("body");
    
    // console.log("width " + size);
    // console.log("height " + height);
   
    // console.log(bbody);

   const windowHeight = window.innerHeight;
   const windowWidth = window.innerWidth;
//    console.log("window height " + windowHeight);
//    console.log("window width " + windowWidth);

//    fireNotif('screen size','success', 5000);
    return {clientWidth: size,clientHeight:height,innerHeight: windowHeight,innerWidth: windowWidth}
}

// For testing purposes, add 100 rows of data to the db
    function loadErUp() {
        console.log("loading");
      let list = [];

        for (let i = 0; i < 100; i++) {
            //create the object
            let expense = {
                Month: "August",
                Week: "1",
                Type: "1",
                Amount: "12"+i,
                Date: "2024-8-7",
                Description: "insert " + i,
                Currency: "$"
            }

            list.push(expense);
        }

        const obj = JSON.stringify(list);

         $.ajax({

            // Our sample url to make request 
            url:
                '/Expenses/Create',

            // Type of Request
            contentType: 'application/json; charset=utf-8',
            type: "Post",
            data: obj,

            // Function to call when to
            // request is ok 
            success: function (data) {
                window.localStorage.setItem("Success", "Successfully Added Expenses");
                let x = JSON.stringify(data);
                console.log("success", x);
              //  const form = document.getElementById("expenseForm");
                // refresh the form, but keep current month
                //form.submit();
            },

            // Error handling 
            error: function (error) {
                //console.log(`Error ${error}`);
                fireNotif('There was an Error Adding Expenses', 'error', 5000);
                console.log(error)
            }
        });
    }
