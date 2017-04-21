// Initialize tooltips
$(document).ready(function () {
	$('[data-toggle="tooltip"]').tooltip();
});

// Initialize

$(document).ready(function () {

	var allSliderContainers = $('.calculator-slider');

	for (var i = 0; i < allSliderContainers.length; i++) {
		var container = $(allSliderContainers[i]);
		var slider = container.find('.slider-range-min');
		var amountInput = container.find('input.amount');

		var min = slider.data('min');
		var max = slider.data('max');
		var defaultValue = slider.data('default');

		amountInput.val(defaultValue + "cm");

		$(slider[0]).slider({
			range: "min",
			value: defaultValue,
			animate: true,
			min: min,
			max: max,
			slide: function (event, ui) {
				$(event.target).parents('.calculator-slider').find('input.amount').val(ui.value + "cm");
			}
		});
	}


	var rowMultiplierBtns = $('.js-row-multiplier');
	for (var i = 0; i < rowMultiplierBtns.length; i++) {
		var btn = $(rowMultiplierBtns[i]);
		btn.on('click', function () {
			var clickedBtn = $(this);
			var row = clickedBtn.parents('.row').next('.js-calculator-row')
			var clonedRow = row.clone();
			clonedRowSliders = clonedRow.find('.slider-range-min');

			for (var y = 0; y < clonedRowSliders.length; y++) {
				var slider = $(clonedRowSliders[y]);

				var min = slider.data('min');
				var max = slider.data('max');
				var defaultValue = slider.data('default');

				slider.slider({
					range: "min",
					value: defaultValue,
					animate: true,
					min: min,
					max: max,
					slide: function (event, ui) {
						$(event.target).parents('.calculator-slider').find('input.amount').val(ui.value + "cm");
					}
				});
			}

			clonedRow.insertAfter(row);
		})
	}

	// material selection logic

	var baseMaterials = $('.js-base-surface-material');
	var doorMaterials = $('.js-door-surface-material');
	var fazerMaterials = $('.js-fazer-surface-material');
	var handleMaterials = $('.js-handle-material');

	attachEventHandlers(baseMaterials);
	attachEventHandlers(doorMaterials);
	attachEventHandlers(fazerMaterials);
	attachEventHandlers(handleMaterials);

	function attachEventHandlers(materials) {
		for (var i = 0; i < materials.length; i++) {
			$(materials[i]).on('click', function () {
				var material = $(this);
				dimMaterials(materials);
				material.addClass('selected');
			});
		}
	}

	function dimMaterials(materials) {
		for (var i = 0; i < materials.length; i++) {
			var material = $(materials[i]);
			material.addClass('dimmed');
			material.removeClass('selected');
		}
	}



	// Calculation logic
	$('.js-calculate-btn').on('click', function () {
		var errors = [];

		// Materials
		var baseMaterial = $('.js-base-surface-material.selected');
		var doorMaterial = $('.js-door-surface-material.selected');
		var fazerMaterial = $('.js-fazer-surface-material.selected');
		var handleMaterial = $('.js-handle-material.selected');

		// МАТЕРИАЛИ
		var baseMaterialPrice = baseMaterial.data('price');
		var doorMaterialPrice = doorMaterial.data('price');
		var fazerMaterialPrice = fazerMaterial.data('price');
		var handleMaterialPrice = handleMaterial.data('price');

		if (!baseMaterial[0]) {
			errors.push({ index: 1, message: " *Не е избрана плоча за основа"});
		}

		if (!doorMaterial[0]) {
			errors.push({ index: 2, message: " *Не е избрана плоча за врата"});
		}

		if (!fazerMaterial[0]) {
			errors.push({ index: 3, message: " *Не е избрана плоча за фазер"});
		}

		if (!handleMaterial[0]) {
			errors.push({ index: 4, message: " *Не е избрана дръжка"});
		}

		// ВЪНШЕН РАЗМЕР, ЕЛЕМЕНТИ И ЦОКЪЛ
		var outerSizeHeight = parseInt($('.js-outer-size-height').val());
		var outerSizeWidth = parseInt($('.js-outer-size-width').val());
		var outerSizeDepth = parseInt($('.js-outer-size-depth').val());
		var outerSizeCount = parseInt($('.js-outer-size-count').val());
		var outerSizeCokal = $('.js-outer-size-cokal').val() === 'Да' ? true : false;

		// ВРАТИ 
		var doorPrices = getPriceData('.js-doors-row', ['height', 'width', 'count']);

		// ФИКСИРАНИ ВЪТРЕШНИ ДЕЛЕНИЯ:
		var innerDividersPrices = getPriceData('.js-inner-dividers-row', ['length', 'count']);

		// РАФТОВЕ:
		var shelfPrices = getPriceData('.js-shelf-row', ['length', 'count']);

		// ЧЕКМЕДЖЕ:
		var drawerPrices = getPriceData('.js-drawer-row', ['height', 'width', 'count']);

		// ЗАКАЧАЛКИ:
		var hangerPrice = parseInt($('.js-hanger-type').val());
		var hangerCount = parseInt($('.js-hanger-count').val());

		// ОГЛЕДАЛА:
		var mirrorPrices = getPriceData('.js-mirror-row', ['height', 'width', 'count']);

		if (errors.length == 0) {
			var allErrorMessages = $('.js-calculator-error').hide();

			var data = {
				constants: {
					laborPricePercent: 50,
					cuttingPricePercent: 5
				},
				baseMaterialPrice: baseMaterialPrice,
				doorMaterialPrice: doorMaterialPrice,
				fazerMaterialPrice: fazerMaterialPrice,
				handleMaterialPrice: handleMaterialPrice,
				outerSizeHeight: outerSizeHeight,
				outerSizeWidth: outerSizeWidth,
				outerSizeDepth: outerSizeDepth, 
				outerSizeCount: outerSizeCount, 
				outerSizeCokal: outerSizeCokal ,
				doorPrices: doorPrices,
				innerDividersPrices: innerDividersPrices,
				shelfPrices: shelfPrices,
				drawerPrices: drawerPrices,
				hangerPrice: hangerPrice,
				hangerCount : hangerCount,
				mirrorPrices : mirrorPrices
			};

			var result = calculateResult(data);

			displayResult(result);

		} else {
			displayErrors(errors);
		}
	});

	function calculateResult(data) {
		console.log(data);
		return 400;
	}

	function displayErrors(errors) {

		var scrollTarget = $('.calculator-container');
		$('html, body').animate({
			scrollTop: scrollTarget.offset().top - 150
		}, 200);

		var allErrorMessages = $('.js-calculator-error').hide();
		for (var i = 0; i < errors.length; i++) {
			var error = errors[i];
			var errorElement = $('.js-calculator-error[data-error-index=' + error.index + ']');
			errorElement.show();
			errorElement.text(error.message);
		}
	}

	function displayResult(result) {
		var installationPercent = 1.07;
		var includeInstallation = $('#installationCb').is(':checked');

		if (includeInstallation) {
			result = result * installationPercent;
		}

		$('.js-result').val(result + ' лв');
	}

	function getPriceData(rowsSelector, fields) {
		var data = [];
		var rows = $(rowsSelector);
		for (var i = 0; i < rows.length; i++) {
			var row = $(rows[i]);

			var priceObject = {};

			for (var y = 0; y < fields.length; y++) {
				var fieldName = fields[y];
				var fieldValue = parseInt(row.find('.js-' + fieldName).val());

				priceObject[fieldName] = fieldValue;
			}

			data.push(priceObject);
		}

		return data;
	}
});


// Calculation
