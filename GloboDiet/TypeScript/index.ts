class CascadingDropdowns {
    private readonly _primaryDropdown : HTMLSelectElement;
    private readonly _secondaryDropdown: HTMLSelectElement;

    constructor(primaryDropdownId: string, secondaryDropdownId: string) {
        this._primaryDropdown = document.getElementById(primaryDropdownId) as HTMLSelectElement;
        //$("#primary").
        this._secondaryDropdown = document.getElementById(secondaryDropdownId) as HTMLSelectElement;
        this._primaryDropdown.addEventListener("change", this.onPrimaryChange.call(this));
    }

    private onPrimaryChange(): void {
        console.log(this);
        if (this._primaryDropdown) {
        } else {
            // 2nd dropdown wieder leeren
            $(this._secondaryDropdown).empty();
        }

    }

}
