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

	// Toggle sections
	var mirrorToggleCb = $('.js-section-toggle');
	mirrorToggleCb.on('change', function () {
		var isChecked = $(this).is(':checked');
		var section = $(this).parents('section.calculator-section');
		var rows = section.find('.js-calculator-row');
		if (isChecked) {
			rows.show();
		} else {
			rows.hide();
		}
	})

	// Toggle doors type
	var doorsCountDdlItems = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
	var doorsCountDdlItemsSliding = [2, 3, 4];

	var doorTypeToggle = $('.js-door-type');
	var doorsCountDdl = $('.js-doors-count');
	doorTypeToggle.on('change', function () {
		var type = doorTypeToggle.val();
		var cols = $('.js-doors-col');

		if (type == 1) {
			cols.show();
			replaceDdlItems(doorsCountDdl, doorsCountDdlItems);
		}
		if (type == 2) {
			cols.hide();
			replaceDdlItems(doorsCountDdl, doorsCountDdlItemsSliding);
		}
	})

	function replaceDdlItems(ddl, items) {
		ddl.empty();
		for (var i = 0; i < items.length; i++) {
			ddl.append("<option>" + items[i] + "</option>");
		}
	}

	// Remove row logic
	$('.js-remove-row').on('click', removeRow);

	function removeRow() {
		var clickedBtn = $(this);
		console.log(clickedBtn);
		var section = clickedBtn.parents('section.calculator-section');
		var sectionRowsCount = section.find('.js-calculator-row').length;
		if (sectionRowsCount > 1) {
			var rowToDelete = clickedBtn.parents('.row');
			rowToDelete.remove();
		} else {
			alert("Не може да изтривате когато имате само 1 ред!");
		}
	}

	var rowMultiplierBtns = $('.js-row-multiplier');
	for (var i = 0; i < rowMultiplierBtns.length; i++) {
		var btn = $(rowMultiplierBtns[i]);
		btn.on('click', function () {
			var clickedBtn = $(this);
			var row = clickedBtn.parents('.row').next('.js-calculator-row')
			var clonedRow = row.clone();

			clonedRowDeleteButtons = clonedRow.find('.js-remove-row');

			for (var i = 0; i < clonedRowDeleteButtons.length; i++) {
				var btn = $(clonedRowDeleteButtons[i]);
				btn.on('click', removeRow);
			}

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
			errors.push({ index: 1, message: " *Не е избрана плоча за основа" });
		}

		if (!doorMaterial[0]) {
			errors.push({ index: 2, message: " *Не е избрана плоча за врата" });
		}

		if (!fazerMaterial[0]) {
			errors.push({ index: 3, message: " *Не е избрана плоча за фазер" });
		}

		if (!handleMaterial[0]) {
			errors.push({ index: 4, message: " *Не е избрана дръжка" });
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
		if ($('.js-dividers-toggle').is(':checked')) {
			var innerDividersPrices = getPriceData('.js-inner-dividers-row', ['length', 'count']);
		}

		// РАФТОВЕ:
		if ($('.js-shelfs-toggle').is(':checked')) {
			var shelfPrices = getPriceData('.js-shelf-row', ['length', 'count']);
		}

		// ЧЕКМЕДЖЕ:
		if ($('.js-drawers-toggle').is(':checked')) {
			var drawerPrices = getPriceData('.js-drawer-row', ['height', 'width', 'count']);
		}

		// ЗАКАЧАЛКИ:
		if ($('.js-hangers-toggle').is(':checked')) {
			var hangerPrices = getPriceData('.js-hanger-row', ['hangerType', 'hangerCount']);
		}

		// ОГЛЕДАЛА:

		if ($('.js-mirrors-toggle').is(':checked')) {
			var mirrorPrices = getPriceData('.js-mirror-row', ['height', 'width', 'count']);
		}

		if (errors.length == 0) {
			var allErrorMessages = $('.js-calculator-error').hide();

			var data = {
				constants: {
					laborPricePercent: 1.5, // процент
					cuttingPrice: 3.59, // лв умножава се по квадратите на изразходвания материал
					cant2MM: 1, // лв кант врати
					cant08MM: 0.56, // лв кант други
					kracheta: 3, // лв добавяме ги когато има цокъл
					montajKant2MM: 0.71, // лв 
					montajKant08MM: 0.59, // лв 
					panta: 1.8, // лв цена панта
					mehanizam2Vrati: 170, // лв 
					mehanizam3Vrati: 250, // лв 
					mehanizam4Vrati: 300, // лв 
					mirrorPrice: 35, // лв m2 за огледало
					mehanizamDrawer: 15, // лв механизам чекмедже
					trudDrawer: 5, // лв труд чекмедже - сглабяне
					slidingDoorPrice: 20 // лв цена за плъзгаща се врата
				},
				baseMaterialPrice: baseMaterialPrice,
				doorMaterialPrice: doorMaterialPrice,
				fazerMaterialPrice: fazerMaterialPrice,
				handleMaterialPrice: handleMaterialPrice,
				outerSizeHeight: outerSizeHeight,
				outerSizeWidth: outerSizeWidth,
				outerSizeDepth: outerSizeDepth,
				outerSizeCount: outerSizeCount,
				outerSizeCokal: outerSizeCokal,
				doorPrices: doorPrices,
				innerDividersPrices: innerDividersPrices,
				shelfPrices: shelfPrices,
				drawerPrices: drawerPrices,
				hangerPrices: hangerPrices,
				mirrorPrices: mirrorPrices
			};

			var result = calculateResult(data);

			displayResult(result);

		} else {
			displayErrors(errors);
		}
	});

	function calculateResult(data) {
		console.log('------------------------');
		// ВЪНШНИ ГАБАРИТИ
		var d = data.outerSizeDepth;
		var h = data.outerSizeHeight;
		var w = data.outerSizeWidth;
		var c = data.outerSizeCount;

		var outerSize = ((2 * (d * h)) + (2 * (d * w))) / 10000;
		var elementsSize = ((h * d) * (c - 1)) / 10000;
		var cokalSize = 0;
		var kantSize = (((d * 4) + (w * 2) + (h * 2)) + (h * (c - 1))) / 100;


		if (data.outerSizeCokal) {
			cokalSize = (10 * data.outerSizeWidth) / 10000;
			kantSize = (((d * 4) + (w * 3) + (h * 2)) + (h * (c - 1))) / 100;
		}

		var M = kantSize * (data.constants.cant08MM + data.constants.montajKant08MM); // лв Цена кант
		var Y = (outerSize + elementsSize + cokalSize) * data.constants.cuttingPrice; // лв Такса рязане
		var S = ((h * w) / 10000) * data.fazerMaterialPrice; // лв Цена Фазер
		var L = 0;

		if (data.outerSizeCokal) {
			L = (((outerSize + elementsSize + cokalSize) * data.baseMaterialPrice) * data.constants.laborPricePercent) + data.constants.kracheta;
		}
		else {
			L = (((outerSize + elementsSize + cokalSize) * data.baseMaterialPrice) * data.constants.laborPricePercent);
		}

		var totalOuterPrice = M + Y + S + L;

		console.log("Цена външни габарити: " + totalOuterPrice);

		// ДРЪЖКИ
		var doorsTotalCount = getDoorsTotalCount(data.doorPrices);
		var totalHandlesPrice = data.handleMaterialPrice * doorsTotalCount;

		console.log("Цена дръжки: " + totalHandlesPrice);


		// ВРАТИ
		var totalDoorsPrice = 0;
		if ($('.js-door-type').val() == 1) {
			for (var i = 0; i < data.doorPrices.length; i++) {
				var currentDoorData = data.doorPrices[i];
				totalDoorsPrice += calculateDoorPrice(currentDoorData, data);
			}

		} else if ($('.js-door-type').val() == 2) {
			for (var i = 0; i < data.doorPrices.length; i++) {
				var currentDoorData = data.doorPrices[i];
				totalDoorsPrice += calculateSlidingDoorPrice(currentDoorData, data);
			}
		}

		console.log("Обща цена (врати): " + totalDoorsPrice);


		// ФИКСИРАНИ ВЪТРЕШНИ ДЕЛЕНИЯ
		var totalDividersPrice = 0;
		if (data.innerDividersPrices) {
			for (var i = 0; i < data.innerDividersPrices.length; i++) {
				var currentDividerData = data.innerDividersPrices[i];
				totalDividersPrice += calculateDividerPrice(currentDividerData, data);
			}
		}

		console.log("Обща цена (разделители): " + totalDividersPrice);

		// РАФТОВЕ
		var totalShelfsPrice = 0;
		if (data.shelfPrices) {
			for (var i = 0; i < data.shelfPrices.length; i++) {
				var currentShelfData = data.shelfPrices[i];
				totalShelfsPrice += calculateShelfPrice(currentShelfData, data);
			}
		}

		console.log("Обща цена (рафтове): " + totalShelfsPrice);

		// ЧЕКМЕДЖЕТА
		var totalDrawerPrice = 0;
		if (data.drawerPrices) {
			for (var i = 0; i < data.drawerPrices.length; i++) {
				var currentDrawerData = data.drawerPrices[i];
				totalDrawerPrice += calculateDrawerPrice(currentDrawerData, data);
			}
		}

		console.log("Обща цена (чекмеджета): " + totalDrawerPrice);

		// ЗАКАЧАЛКИ
		var totalHangerPrice = 0;
		if (data.hangerPrices) {
			for (var i = 0; i < data.hangerPrices.length; i++) {
				totalHangerPrice += data.hangerPrices[i].hangerType * data.hangerPrices[i].hangerCount;
			}
		}

		console.log("Обща цена (закачалки): " + totalHangerPrice);

		// ОГЛЕДАЛА
		var totalMirrorsPrice = 0;
		if (data.mirrorPrices) {
			for (var i = 0; i < data.mirrorPrices.length; i++) {
				var currentMirrorData = data.mirrorPrices[i];
				totalMirrorsPrice += calculateMirrorPrice(currentMirrorData, data);
			}
		}

		console.log("Обща цена (огледала): " + totalMirrorsPrice);

		var TOTALPRICE = totalOuterPrice + totalHandlesPrice + totalDoorsPrice + totalDividersPrice + totalShelfsPrice + totalDrawerPrice + totalHangerPrice + totalMirrorsPrice;

		console.log("TOTAL PRICE: " + TOTALPRICE);

		return TOTALPRICE;
	}

	function getDoorsTotalCount(doorPrices) {
		var totalCount = 0;
		for (var i = 0; i < doorPrices.length; i++) {
			totalCount += doorPrices[i].count;
		}

		return totalCount;
	}

	function calculateMirrorPrice(mirrorData, data) {
		var h = mirrorData.height;
		var w = mirrorData.width;
		var c = mirrorData.count;

		var a = ((h * w) * c) / 10000;

		var V = (a * data.constants.mirrorPrice) + (c * 5);

		return V;
	}

	function calculateDrawerPrice(drawerData, data) {
		var h = drawerData.height;
		var w = drawerData.width;
		var d = data.outerSizeDepth;
		var c = drawerData.count;
		var p = data.constants.mehanizamDrawer;
		var r = data.constants.trudDrawer;

		var a = (((3 * (w * h)) + (2 * (d * h))) * c) / 10000;
		var b = (2 * (w + h)) / 100;
		var i = b * (data.constants.cant2MM + data.constants.montajKant2MM);
		var f = ((2 * (w + h)) + (2 * (h + d))) / 100;
		var m = f * (data.constants.cant08MM + data.constants.montajKant08MM);

		var M = c * (m + i);
		var Y = a * data.constants.cuttingPrice;
		var q = (c * (w * d)) / 10000;
		var T = q * data.fazerMaterialPrice;

		var Q = (a * data.baseMaterialPrice) + ((p + r) * c);

		var V = Q + M + Y + T;

		return V;
	}

	function calculateShelfPrice(shelfData, data) {
		var l = shelfData.length;
		var c = shelfData.count;

		var d = data.outerSizeDepth;
		var a = ((l * d) * c) / 10000;
		var b = (l * c) / 100;

		var M = b * (data.constants.cant08MM + data.constants.montajKant08MM);
		var Y = a * data.constants.cuttingPrice;
		var Q = (a * data.baseMaterialPrice) * data.constants.laborPricePercent;
		var V = Q + M + Y;

		return V;
	}

	function calculateDividerPrice(dividerData, data) {
		var l = dividerData.length;
		var c = dividerData.count;

		var d = data.outerSizeDepth;
		var a = ((l * d) * c) / 10000;
		var b = (l * c) / 100;

		var M = b * (data.constants.cant08MM + data.constants.montajKant08MM);
		var Y = a * data.constants.cuttingPrice;
		var Q = (a * data.baseMaterialPrice) * data.constants.laborPricePercent;
		var V = Q + M + Y;

		return V;
	}

	function calculateSlidingDoorPrice(doorData, data) {

		var h = data.outerSizeHeight;
		var w = data.outerSizeWidth;
		var c = doorData.count;

		var b = 0;

		switch (c) {
			case 2:
				b = data.constants.mehanizam2Vrati;
				break;
			case 3:
				b = data.constants.mehanizam3Vrati;
				break;
			case 4:
				b = data.constants.mehanizam4Vrati;
				break;
			default:
				b = data.constants.mehanizam2Vrati;
				break;
		}

		var a = (h * w) / 10000;
		var y = (2 * w) / 100;

		var M = y * (data.constants.cant08MM + data.constants.montajKant08MM);
		var Y = a * data.constants.cuttingPrice;
		var V = ((a * data.doorMaterialPrice) * data.constants.laborPricePercent) + b;

		var D = M + Y + V;

		return D;
	}

	function calculateDoorPrice(doorData, data) {
		var h = doorData.height;
		var w = doorData.width;
		var c = doorData.count;

		var p = 0;

		if (h > 200) {
			p = 5;
		} else if (h > 100 && h <= 200) {
			p = 4;
		} else if (h <= 100) {
			p = 3;
		}

		var a = ((h * w) * c) / 10000;
		var b = ((2 * (h + w)) * c) / 100;

		var M = b * (data.constants.cant2MM + data.constants.montajKant2MM);
		var Y = a * data.constants.cuttingPrice;
		var Q = ((a * data.doorMaterialPrice) * data.constants.laborPricePercent) + (p * data.constants.panta);
		var V = Q + M + Y;

		return V;
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
		var installationPercent = 0.07;
		var installationPrice = 0;
		var includeInstallation = $('#installationCb').is(':checked');

		if (includeInstallation) {
			installationPrice = result * installationPercent;
		}

		if (includeInstallation && result <= 400) {
			installationPrice = 25;
		}

		$('.js-result').val(parseFloat(result).toFixed(2) + ' лв');
		$('.js-result-installation').val(parseFloat(installationPrice).toFixed(2) + ' лв');
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
