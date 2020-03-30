var geocoder;var map;var infowindow=new google.maps.InfoWindow();var bounds=new google.maps.LatLngBounds();function initialize(lat,lng,infoWindowContent,mapCanvas,markerIconUrl){if(!mapCanvas)mapCanvas="map_canvas";geocoder=new google.maps.Geocoder();var latlng=new google.maps.LatLng(lat,lng);var myOptions={zoom:15,center:latlng,mapTypeId:google.maps.MapTypeId.ROADMAP,scrollwheel:true}
map=new google.maps.Map(document.getElementById(mapCanvas),myOptions);var marker=new google.maps.Marker({position:latlng,map:map,icon:markerIconUrl});google.maps.event.addListener(marker,'click',function(event){OpenInfoBox(map,marker,infoWindowContent);});google.maps.event.addListener(map,'click',function(event){codeLatLng(event.latLng);});bounds.extend(marker.getPosition());}function addMarker(lat2,lng2,infoWindowContent2,markerIconUrl){var latlng2=new google.maps.LatLng(lat2,lng2);var marker2=new google.maps.Marker({position:latlng2,map:map,icon:markerIconUrl});google.maps.event.addListener(marker2,'click',function(event){OpenInfoBox(map,marker2,infoWindowContent2);});bounds.extend(marker2.getPosition());map.fitBounds(bounds);}function OpenInfoBox(map,marker,boxText){var myOptions={content:boxText,disableAutoPan:false,maxWidth:0,pixelOffset:new google.maps.Size(-170,0),zIndex:null,boxStyle:{},closeBoxMargin:"4px 4px 4px 4px",closeBoxURL:"http://www.google.com/intl/en_us/mapfiles/close.gif",infoBoxClearance:new google.maps.Size(1,1),isHidden:false,pane:"floatPane",enableEventPropagation:false};var ib=new InfoBox(myOptions);ib.open(map,marker);}function codeAddress(){var address=document.getElementById("address").value;geocoder.geocode({'address':address},function(results,status){if(status==google.maps.GeocoderStatus.OK){map.setCenter(results[0].geometry.location);var marker=new google.maps.Marker({map:map,position:results[0].geometry.location});infowindow.setContent(results[0].formatted_address);infowindow.open(map,marker);google.maps.event.addListener(marker,'click',function(){infowindow.setContent(results[0].formatted_address);infowindow.open(map,marker);FillValueToControls(latlng.lat(),latlng.lng(),results[0].formatted_address);});FillValueToControls(results[0].geometry.location.lat(),results[0].geometry.location.lng(),address);}else{alert("Không tìm thấy địa chỉ bạn vừa nhập. Vui lòng thử địa chỉ khác hoặc kích chọn trực tiếp trên bản đồ.");}});}function codeLatLng(latlng){geocoder.geocode({'latLng':latlng},function(results,status){if(status==google.maps.GeocoderStatus.OK){if(results[1]){var marker=new google.maps.Marker({position:latlng,map:map});infowindow.setContent(results[0].formatted_address);infowindow.open(map,marker);google.maps.event.addListener(marker,'click',function(){infowindow.setContent(results[0].formatted_address);infowindow.open(map,marker);FillValueToControls(latlng.lat(),latlng.lng(),results[0].formatted_address);});FillValueToControls(latlng.lat(),latlng.lng(),results[0].formatted_address);}}else{alert("Geocoder failed due to: "+status);}});}function FillValueToControls(lat,lng,address){}function onEnterPress(buttonName,e){var key;if(window.event)key=window.event.keyCode;else
key=e.which;if(key==13){var btn=document.getElementById(buttonName);if(btn!=null){btn.click();event.keyCode=0}}}