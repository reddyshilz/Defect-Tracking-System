//// Activates the Carousel
//$('.carousel').carousel({
//  interval: 5000
//})

//// Activates Tooltips for Social Links
//$('.tooltip-social').tooltip({
//  selector: "a[data-toggle=tooltip]"
//})

$('.dropdown').hover(function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideDown(150);
}, function () {
    $(this).find('.dropdown-menu').first().stop(true, true).slideUp(105);
});