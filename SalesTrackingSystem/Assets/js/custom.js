tinymce.init({
    selector: ".textarea-init",
    plugins: 'print preview searchreplace autolink directionality  visualblocks visualchars fullscreen image link media template code codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists wordcount  imagetools textpattern help ',
    toolbar: 'formatselect | bold italic strikethrough forecolor backcolor permanentpen formatpainter | link image media pageembed | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent | removeformat | addcomment',
    paste_data_images: true,
    menubar: true,
    height: 500,
    theme: 'silver',
    mobile: {
        theme: 'mobile',
        plugins: ['autosave', 'lists', 'autolink'],
        toolbar: ['undo', 'bold', 'italic', 'styleselect']
    }
});

$(".bod-picker").nepaliDatePicker({
    dateFormat: "%y-%m-%d",
    closeOnDateSelect: true
});

$("#clear-DateProduce").on("click", function (event) {
    $("#DateProduced").val('');
});
$("#clear-ExpiryDate").on("click", function (event) {
    $("#ExpiryDate").val('');
});

$('.dropify').dropify({
    messages: {
        'default': 'Drag and drop a file here or click',
        'replace': 'Drag and drop or click to replace',
        'remove': 'Remove',
        'error': 'Ooops, something wrong happended.'
    },
    error: {
        'fileSize': 'The file size is too big ({{ value }} max).',
        'minWidth': 'The image width is too small ({{ value }}}px min).',
        'maxWidth': 'The image width is too big ({{ value }}}px max).',
        'minHeight': 'The image height is too small ({{ value }}}px min).',
        'maxHeight': 'The image height is too big ({{ value }}px max).',
        'imageFormat': 'The image format is not allowed ({{ value }} only).'
    }
});

setTimeout(function () {
    $('.alert').slideUp();
}, 5000);
