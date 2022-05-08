import {getCookie} from "typescript-cookie";

export default function authHeader() : {} {
	const token = getCookie('access_token');
	if (token === undefined) return {};
	return {"Access-Token": token};
}