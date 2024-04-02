import axios from "axios";

const url = import.meta.env.VITE_API_URL === undefined ? `` : import.meta.env.VITE_API_URL;

export default axios.create({
  baseURL: `${url}`
})
