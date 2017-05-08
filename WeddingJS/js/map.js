function initMap() {
        var uluru = {lat: 43.8247, lng: -79.2786};
        var map = new google.maps.Map(document.getElementById('chapelMap'), {
          zoom: 16,
          center: uluru,
          scrollwheel: false
        });
        
  var contentString = '<div id="content">'+
            '<div id="siteNotice">'+
            '</div>'+
            '<div id="bodyContent">'+
            '<address>'+
            '<strong>Prince of Peace Church</strong><br />'+
            '255 Alton Tower Circle<br />Scarborough, ON M1V 4E7<br><abbr title="Phone">P:</abbr> (416) 393-5416'+
            '</address>'+
            '</div>'+
            '</div>';
        
  var infowindow = new google.maps.InfoWindow({
          content: contentString
  });      
        
  var marker = new google.maps.Marker({
          position: uluru,
          map: map,
          title: 'Chapel Location'
  });
  
  marker.addListener('click', function() {
          infowindow.open(map, marker);
  });
  
  infowindow.open(map, marker);
}