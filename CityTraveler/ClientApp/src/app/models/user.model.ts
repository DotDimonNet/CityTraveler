export interface IUserProfile {
    name: string,
    userName: string,
    phoneNumber: string,
    email: string,
    avatar: string,
    useerId: string
}

export class UserProfile {
    static doSomething(a?: boolean): string {
        return "";
    }

    dod() {
        UserProfile.doSomething();
    }
}