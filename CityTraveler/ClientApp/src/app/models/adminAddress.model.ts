export interface IAdminAddress {
    houseNumber: string,
    apartmentNumber: string,
    id: string,
    title: string
}

export class AdminAddress {
    static doSomething(a?: boolean): string {
        return "";
    }

    dod() {
        AdminAddress.doSomething();
    }
}