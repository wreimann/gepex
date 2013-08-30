var j = jQuery.noConflict();
j(function () {
    j('#gallery_img li').each(function (idx) {
        j(this).data('index', (++idx));
    });

    j('#gallery_img').jcarousel({
        scroll: 5,
        vertical: true,
        initCallback: initCallbackFunction
    })

    function initCallbackFunction(carousel) {
        j('#img').bind('image-loaded', function () {
            var idx = j('#gallery_img li.active').data('index') - 2;

            carousel.scroll(idx);
            return false;
        });

        // hotkeys plugin: use arrows to control the gallery
        j(document).bind('keydown', 'right', function (evt) { j.galleria.next(); });
        j(document).bind('keydown', 'left', function (evt) { j.galleria.prev(); });
        j(document).bind('keydown', 'up', function (evt) { j('.jcarousel-next-vertical').click(); return false; });
        j(document).bind('keydown', 'down', function (evt) { j('.jcarousel-prev-vertical').click(); return false; });
    };

    // load and fade-in thumbnails
    j('#gallery_img li img').css('opacity', 0).each(function () {
        if (this.complete || this.readyState == 'complete') { j(this).animate({ 'opacity': 1 }, 300) }
        else { j(this).load(function () { j(this).animate({ 'opacity': 1 }, 300) }); }
    });
    j('#gallery_img li span').css('opacity', 0).each(function () {
        if (this.complete || this.readyState == 'complete') { j(this).animate({ 'opacity': 1 }, 300) }
        else { j(this).load(function () { j(this).animate({ 'opacity': 1 }, 300) }); }
    });

    j('#gallery_img').galleria({
        // #img is the empty div which holds full size images
        insert: '#img',

        // enable history plugin
        history: true,

        // function fired when the image is displayed
        onImage: function (image, caption, thumb) {
            // fade in the image 
            image.hide().fadeIn(500);

            // animate active thumbnail's opacity to 1, other list elements to 0.6
            thumb.parent().fadeTo(200, 1).siblings().fadeTo(200, 0.6)

            // j('#img').data('currentIndex', jli.data('index')).trigger('image-loaded')

            j('#img')
                .trigger('image-loaded')
                .hover(
                    function () { j('#img .caption').stop().animate({ height: 50 }, 250) },
                    function () {
                        if (!j('#show-caption').is(':checked')) {
                            j('#img .caption').stop().animate({ height: 0 }, 250)
                        }
                    }
                );
        },

        // function similar to onImage, but fired when thumbnail is displayed
        onThumb: function (thumb) {
            var jli = thumb.parent(),
                opacity = jli.is('.active') ? 1 : 0.6;

            // hover effects for list elements
            jli.hover(
                function () { jli.fadeTo(200, 1); },
                function () { jli.not('.active').fadeTo(200, opacity); }
            )
        }
    }).find('li:first').addClass('active') // display first image when Galleria is loaded

    j('#img .caption').css('height', 0)

    j('#slideshow').hide()

    // this one is for Firefox, which loves to leave fields checked after page refresh
    j('#toggle-slideshow, #show-caption').removeAttr('checked')

    j('#show-caption').change(function () {
        if (this.checked) {
            j('#img .caption').stop().animate({ height: 50 }, 250)
        } else {
            j('#img .caption').stop().animate({ height: 0 }, 250)
        }
    })


    var slideshow,
        slideshowPause = j('#slideshow-pause').val()

    j('#slideshow-pause').change(function () {
        slideshowPause = this.value

        // clear interval when timeout is changed
        window.clearInterval(slideshow)

        // and set new interval with new timeout value
        slideshow = window.setInterval(function () {
            j.galleria.next()
        }, slideshowPause * 1000) // must be set in milisecond
    })

    j('input#toggle-slideshow').change(function () {
        if (this.checked) {
            j('#slideshow').fadeIn()

            // set interval when slideshow is enabled
            slideshow = window.setInterval(function () {
                j.galleria.next()
            }, slideshowPause * 1000)
        } else {
            j('#slideshow').fadeOut()

            // clear interval when slideshow if disabled
            window.clearInterval(slideshow)
        }
    })
});


