declare type Client = {
	id: number
	lastName: string,
	firstName: string,
	middleName: string | null,
	email: string,
	phone: string,
	login: string,
	company: string | null,
	avatar: string | undefined
}
export default Client;