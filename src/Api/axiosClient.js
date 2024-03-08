import axios from 'axios'

const ServerUrl = "http://localhost:5241"
const token = window ? window.localStorage.getItem("token"):"";

const axiosClient = axios.create({
    baseURL:"http://localhost:5241",
    headers:{
        Authorization: `Bearer ${token}`
    }
})

export default axiosClient