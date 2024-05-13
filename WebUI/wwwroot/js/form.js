window.nhForm = {
    activeForm: undefined,
    activeLabel: undefined,
    activeElement: undefined,
    activeMenu: undefined,
    activeMenuItems: undefined,
    successRedirect: {},
    _strBuffer: {
        value: '',
        timerId: undefined
    },
    _destroys: [],
    $addDestroy(destroy) {
        this._destroys.push(destroy);
    },
    $cleanupDestroys() {
        if (this._destroys) this._destroys.forEach(destroy => destroy && destroy());
        this._destroys = [];
    },
    onShown() {
        Array.from(document.querySelectorAll('.nh-formBody [oninput*=nhAutoGrow]')).forEach(element => nhAutoGrow(element));
    },
    onSubmit(event) {
        event.preventDefault();

        if (this.recaptchaId !== undefined && !window.grecaptcha.getResponse(this.recaptchaId)) return;

        const url = 'mail.php'; // Оновлено URL для відправки даних
        const xhr = new XMLHttpRequest();
        const form = event.target;
        const formWrapper = form.parentElement;
        const data = new FormData(form);
        const json = {
            'referrerUrl': document.referrer || 'Direct',
            'landingUrl': window.location.href
        };
        data.forEach((value, key) => json[key] = value);
        xhr.open('post', url);
        xhr.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');

        xhr.send(JSON.stringify({nhWebForm: 'v2', fields: json}));

        document.getElementById('nh-form').classList.add('nh-submitted');
        if (this.successRedirect && this.successRedirect.enabled) {
            const icon = formWrapper.querySelector('[nh-component=success-icon]');
            if (icon) icon.setAttribute('class', 'nh-formSuccess-icon-progress');
            setTimeout(() => {
                if (icon) icon.setAttribute('class', 'nh-formSuccess-icon');
                window.open(this.successRedirect.url.startsWith('http') ? this.successRedirect.url : 'https://' + this.successRedirect.url, this.successRedirect.openInNewTab ? '_blank' : '_self');
            }, 3000);
        }
    },
    onChange(event, form) {
        let elementMap = {};
        (form && Array.from(form.elements) || []).forEach(element => {
            const attr = element.getAttribute('nh-required-one-of');
            if (attr) {
                if (elementMap[attr]) return elementMap[attr].push(element);
                elementMap[attr] = [element];
            }
        });
        Object.values(elementMap).forEach(inputs => {
            const isSelectable = ['checkbox', 'radio'].includes(inputs[0].type);
            const required = !inputs.some(input => isSelectable ? input.checked : input.value);

            inputs.forEach(input => {
                if (required) {
                    input.setAttribute('required', '');
                } else {
                    input.removeAttribute('required');
                }
            });
        });
    },
    onFocus(event) {
        const element = event.target;

        if (!element) return;

        this.activeElement = element;
        this.activeLabel =  element.parentElement;
        this.activeForm = this.activeLabel.parentElement;
        this.activeLabel.classList.add('nh-focus');
        const type = element.getAttribute('nh-type');
        if (type) element.type = type;
        this.activeMenu = this.activeLabel.querySelector('[nh-component=menu]');
        if (this.activeMenu) {
            const formClientRect = this.activeForm.getBoundingClientRect();
            const labelClientRect = this.activeLabel.getBoundingClientRect();
            this.activeMenu.style.maxHeight = formClientRect.bottom - labelClientRect.bottom + 'px';
            this.activeMenuItems = Array.from(this.activeMenu.querySelectorAll('[nh-component=menu-item]'));
            const keyDownHandler = this.onMenuKeydown.bind(this);
            this.activeElement.addEventListener('keyup', keyDownHandler);
            this.$addDestroy(() => this.activeElement.removeEventListener('keyup', keyDownHandler));
            const item = this.activeMenuItems.find(item => item.classList.contains('nh-selected'));
            if (item) item.scrollIntoView();
        }
    },
    onPreventEvent(event) {
        event.stopPropagation();
        event.preventDefault();
    },
    onSelectMenuItem(event, item, fieldName) {
        this.onPreventEvent(event);
        this.activeMenuItems.forEach(item => item.classList.remove('nh-selected'));
        item.classList.add('nh-selected');
        const value = item.getAttribute('nh-value');
        const iconUrl = item.getAttribute('nh-icon');
        const title = item.getAttribute('title');
        const input = this.activeLabel.querySelector('[name="' + fieldName + '"]');
        if (input) input.value = value;
        const icon = this.activeLabel.querySelector('[nh-component=dropdown] [nh-component=icon]');
        if (icon) icon.style.setProperty('background', 'url('+ iconUrl + ') center center no-repeat');
        const label = this.activeLabel.querySelector('[nh-component=dropdown] [nh-component=label]');
        if (label) label.innerText = title;
        this.activeElement.blur();
    },
    resetActiveState() {
        this.activeForm = undefined;
        this.activeLabel = undefined;
        this.activeElement = undefined;
        this.activeMenu = undefined;
        this.activeMenuItems = undefined;
    },
    onMenuKeydown(event) {
        this.onPreventEvent(event);
        let keyCode = event.keyCode || event.which;
        let key = event.key.toLowerCase();
        if (keyCode === 0 || keyCode === 229) {
            const pressKey = event.target.value.charAt(event.target.selectionStart - 1);
            keyCode = pressKey.charCodeAt();
            key = pressKey.toLowerCase();
        }
        switch (keyCode) {
            case 9:
            case 13:
            case 17:
            case 18: {
                break;
            }
            case 8: {
                if (this._strBuffer.timerId) clearTimeout(this._strBuffer.timerId);
                this._strBuffer.value = '';
                break;
            }
            case 27: {
                this.activeElement.blur();
                break;
            }
            default: {
                if (this._strBuffer.timerId) clearTimeout(this._strBuffer.timerId);
                this._strBuffer.timerId = setTimeout(() => this._strBuffer.value = '', 300);
                this._strBuffer.value += key;
                const item = this.activeMenuItems.find(item => item.getAttribute('title').toLowerCase().startsWith(this._strBuffer.value));
                if (item) item.scrollIntoView();
            }
        }
    },
    onBlur(event) {
        this.$cleanupDestroys();
        this.resetActiveState();
        const element = event.target;

        if (!element) return;

        const label = element.parentElement;
        label.classList.remove('nh-focus');
        const type = element.getAttribute('nh-type');
        if (type) element.type = 'text';
        if (['RADIO', 'CHECKBOX', 'COUNTRY', 'DATE'].includes((element.getAttribute('type') || '').toUpperCase())) return;
        if (element.value) {
            label.classList.add('nh-notEmpty');
        } else {
            label.classList.remove('nh-notEmpty');
            if (nhForm.isRelElement(element)) {
                const radioElement = nhForm.getSelectableElementByInput(element);
                radioElement.checked = false;
                radioElement.parentElement.classList.remove('nh-selected');
            }
        }
    },
    isRelElement(element) {
        if (!element || element.type === 'radio') return false;
        return !!element.getAttribute('nh-rel');
    },
    getSelectableElementByInput(element) {
        if (!element) return;
        return element.parentElement.parentElement.querySelector('input[nh-rel="' + element.getAttribute('nh-rel') + '"]');
    },
    onSelectableElementChange(element) {
        if (['radio', 'checkbox'].includes(element.type)) {
            const label = element.parentElement;
            const elements = label.parentElement.querySelectorAll('input[name="' + element.name + '"]');
            elements.forEach(button => {
                if (button.checked) {
                    button.parentElement.classList.add('nh-selected');
                } else {
                    button.parentElement.classList.remove('nh-selected');
                }
            });
        }
    },
    onRadioInputFocus(element) {
        if (!element) return;

        const radioElement = nhForm.getSelectableElementByInput(element);
        if (!radioElement) return;
        radioElement.checked = true;
    },
    onRadioInput(element) {
        if (!element) return;

        const radioElement = nhForm.getSelectableElementByInput(element);
        if (!radioElement) return;
        radioElement.checked = true;
        radioElement.value = element.value;
    }
}

function nhRecaptchaInit() {
    const elem = document.getElementById('64a5865bda8e0c0008d5550d-recaptcha');

    if (!elem || !window.grecaptcha) return;

    nhForm.recaptchaId = window.grecaptcha.render('64a5865bda8e0c0008d5550d-recaptcha', {
        'sitekey': '6LfoLh8cAAAAAGbu1VE5OqbT1-J4fmW73XxwiySk',
        'theme': 'light'
    });
}

function nhAutoGrow(element) {
    element.style.height = '5px';
    element.style.height = element.scrollHeight + 'px';
}


window.nhForm.onShown();
window.addEventListener('load', function () {this.parent.postMessage({height: document.body.offsetHeight} ,'*')})
