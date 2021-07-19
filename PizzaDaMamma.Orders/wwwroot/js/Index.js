window.Index = {
    Post: function (url, data, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", url, true);
        xhr.setRequestHeader("Content-Type", "application/json");
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4 && xhr.status === 200) {
                if (callback)
                    callback(xhr.responseText);
            }
        };
        xhr.send(data);
    },
    Data: {
        Items: [],
        Ingridients: []
    },
    Controls: {
        pdmModal: undefined,
    },

    Init: function () {
        let self = window.Index;
        let buttons = document.body.querySelectorAll(".card-table-footer-button");

        self.Controls.pdmModal = document.querySelectorAll('.pdm-modal')[0];

        buttons.forEach(function (button) {
            button.addEventListener("click", function (e) {
                var dataIndex = parseInt(e.target.getAttribute('data-index'));
                self.ShowSelectItem(dataIndex);
            });
        });

        let buttonsClose = document.body.querySelectorAll(".pdm-modal-close");
        buttonsClose.forEach(function (buttonClose) {
            buttonClose.addEventListener("click", function (e) {
                self.Controls.pdmModal.style.display = 'none';
            });
        });
    },

    ShowSelectItem: function (index) {
        let self = window.Index;
        if (index < 0 || index >= self.Data.Items.length)
            return;

        let item = self.Data.Items[index];
        let pdmSelectHeader = document.querySelectorAll('#pdm-pizza-select-header')[0];
        pdmSelectHeader.innerHTML = item.Наименование;

        let pdmSelectImage = document.querySelectorAll('#pdm-pizza-select-image')[0];
        pdmSelectImage.src = "/img/menu/" + item.Изображение;

        let html = '';
        for (let i = 0, icount = item.Ингридиенты.length; i < icount; i++) {
            html += "<span class='pdm-pizza-select-ingridient pdm-pizza-select-ingridient-selected'>" + item.Ингридиенты[i] + "</span>";
        }

        let pdmSelectIngridients = document.querySelectorAll("#pdm-pizza-select-ingridients")[0];
        pdmSelectIngridients.innerHTML = html;


        html = '';
        for (let i = 0, icount = self.Data.Ingridients.length; i < icount; i++) {
            if (item.Ингридиенты.indexOf(self.Data.Ingridients[i]) < 0)
                html += "<span class='pdm-pizza-select-ingridient'>" + self.Data.Ingridients[i] + "</span>";
        }

        let pdmSelectIngridientsAdd = document.querySelectorAll("#pdm-pizza-select-ingridients-add")[0];
        pdmSelectIngridientsAdd.innerHTML = html;



        let buttonsSelect = document.body.querySelectorAll(".pdm-pizza-select-ingridient");
        buttonsSelect.forEach(function (buttonClose) {
            buttonClose.addEventListener("click", self.SelectIngridient);
        });

        self.Controls.pdmModal.style.display = 'block';
    },

    SelectIngridient: function (e) {
        var ingr = e.target.innerHTML;
        if (e.target.hasClass("pdm-pizza-select-ingridient-selected"))
            e.target.removeClass("pdm-pizza-select-ingridient-selected");
        else
            e.target.addClass("pdm-pizza-select-ingridient-selected");
    },
};

HTMLElement.prototype.hasClass = function (cls) {
    var i;
    var classes = this.className.split(" ");
    for (i = 0; i < classes.length; i++) {
        if (classes[i] == cls) {
            return true;
        }
    }
    return false;
};

HTMLElement.prototype.addClass = function (add) {
    if (!this.hasClass(add)) {
        this.className = (this.className + " " + add).trim();
    }
};

HTMLElement.prototype.removeClass = function (remove) {
    var newClassName = "";
    var i;
    var classes = this.className.replace(/\s{2,}/g, ' ').split(" ");
    for (i = 0; i < classes.length; i++) {
        if (classes[i] !== remove) {
            newClassName += classes[i] + " ";
        }
    }
    this.className = newClassName.trim();
};
document.body.onload = function (e) {
    window.Index.Init();
};