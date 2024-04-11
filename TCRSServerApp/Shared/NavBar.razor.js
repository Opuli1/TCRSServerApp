$('.dropdown').on('show.bs.dropdown', function () {
    $('body').append($('.dropdown').css({
        position: 'absolute',
        left: $('.dropdown').offset().left,
        top: $('.dropdown').offset().top
    }).detach());
});

$('.dropdown').on('hidden.bs.dropdown', function () {
    $('.bs-example').append($('.dropdown').css({
        position: false, left: false, top: false
    }).detach());
});