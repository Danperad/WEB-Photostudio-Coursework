export type Answer = {
	status: boolean,
	answer: any,
	error: number | null,
	errorText: string | null
}
export type Client = {
	id: number
	lastName: string,
	firstName: string,
	middleName: string | null,
	email: string,
	phone: string,
	login: string,
	company: string | null
}