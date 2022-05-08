export interface Answer{
	status: boolean,
	answer: any,
	error: number | null,
	errorText: string | null
}

export interface RegistrationModel {
	login: string,
	password: string,
	lastname: string,
	firstname: string,
	phone: string,
	middlename: string,
	email: string
}

export interface LoginModel {
	login: string,
	password: string
}