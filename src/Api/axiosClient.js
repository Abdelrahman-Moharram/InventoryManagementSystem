import axios from 'axios'
import config from 'dotenv'


const token = window ? window.localStorage.getItem("token"):"";

const axiosClient = axios.create({
    baseURL:process.env.SERVER_URL,
    headers:{
        Authorization: `Bearer ${token}`
    }
})

export default axiosClient