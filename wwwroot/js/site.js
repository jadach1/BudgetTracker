// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

var currency_formatter = new Intl.NumberFormat('en-US',{
    style: 'currency',
    currency: 'USD'
})

//Format 2024-5-2 to Thursday May 2024
var listOfMonths = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
var listOfDays = ["Sunday","Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
function humanReadableDate(date){    
    const d = new Date(date);
    let month = listOfMonths[d.getMonth()];
    let day = listOfDays[d.getDay()];
    let year = d.getFullYear();
    return day+" "+month+" "+year;
}

