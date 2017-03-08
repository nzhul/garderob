String.format = function () {
	var s = arguments[0];
	for (var i = 0; i < arguments.length - 1; i++) {
		var reg = new RegExp("\\{" + i + "\\}", "gm");
		s = s.replace(reg, arguments[i + 1]);
	}
	return s;
}

function ChangeImageSource(image, template, materialId) {
	image.css('display', 'block');
	var newSource = String.format(template, materialId);
	image.attr('src', newSource);
}

$(document).ready(function () {
	$('[data-toggle="tooltip"]').tooltip({
		animated: 'fade',
		html: true
	});

	// live preview functionality
	var frontMaterialImage = $('#front-material-image');
	var frontImageSrcTemplate = 'img/live-preview/materials/front/material{0}.png';
	var frontbuttons = $('#front-material-buttons-container img');


	for (var i = 0; i < frontbuttons.length; i++) {
		var button = $(frontbuttons[i]);
		button.on('click', function () {
			var materialId = $(this).data('material-id');
			ChangeImageSource(frontMaterialImage, frontImageSrcTemplate, materialId);
		});
	}

	var backMaterialImage = $('#back-material-image');
	var backImageSrcTemplate = 'img/live-preview/materials/back/material{0}.png';
	var backbuttons = $('#back-material-buttons-container img');

	for (var i = 0; i < backbuttons.length; i++) {
		var button = $(backbuttons[i]);
		button.on('click', function () {
			var materialId = $(this).data('material-id');
			ChangeImageSource(backMaterialImage, backImageSrcTemplate, materialId);
		});
	}
});