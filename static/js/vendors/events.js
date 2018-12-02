//  Get all events by given year and month
function getEvents(yearParam,monthParam){
    $.ajax({
          url: '/events?year=' + yearParam + '&month=' + monthParam,
          type: "GET",
          dataType: "json",
          success: function (data) {
              //console.log(data);
              eventsList = data;
              eventsReady = true;
          },
          error: function (request, status, error) {
            console.log("error");
            //alert(request.responseText);
          }
    });
}

//  Get the current year and month from the server side
function getYearMonth(){
    //  Get current year-month
    $.ajax({
            url: '/events?getYearMonth',
            type: "GET",
            dataType: "json",
            success: function (data) {
            return data;
            },
            error: function (request, status, error) {
            return data;
            //alert(request.responseText);
            }
    });
}