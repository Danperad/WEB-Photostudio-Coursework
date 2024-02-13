import {getCookie} from "typescript-cookie";
import AuthService from "../services/AuthService";

export default function authHeader() : {Authorization: string | undefined} {
	let token = getCookie('access_token');
	if (token === undefined) AuthService.reAuth();
	token = getCookie('access_token');
	if (token === undefined) return {Authorization: undefined};
	return {Authorization: `Bearer ${token}`};
}