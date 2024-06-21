import axios from "axios";

const baseUrl = import.meta.env.VITE_API_URL;
const instant = axios.create({
    baseURL: baseUrl ? baseUrl : `/api/`
})
export default instant