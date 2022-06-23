declare type Client = {
	id: number
	lastName: string,
	firstName: string,
	middleName: string | null,
	eMail: string,
	phone: string,
	login: string,
	avatar: string | undefined
}
export default Client;