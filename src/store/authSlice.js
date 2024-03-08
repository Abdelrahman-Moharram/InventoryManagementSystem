import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import axios from 'axios';
import { jwtDecode } from "jwt-decode";

const server = "http://localhost:5241/api/accounts/"
const token = localStorage.getItem("token");
const initialState = {
    user:token? jwtDecode(token):"",
    token:token? token:"",
    IsAuthenticated: token? true:false,
    IsLoading : false,
    error:null
}

export const AuthLogin = createAsyncThunk( 
    "Auth/Login",
    async (credentials, thunkAPI)=>{
        const {rejectedWithValue} = thunkAPI
        try{
            const response = await axios.post(
                server+"login", 
                credentials, 
                {
                    headers:{"Content-Type":"application/json; charset=UTF-8"}
                })
            if(response.status === 200)
            {
                localStorage.setItem("token", response.data.token)
                return response.data
            }
            return rejectedWithValue(response.data)
            
        }catch(err){
            return rejectedWithValue(err.message)
        }
    })

    export const AuthLogout = createAsyncThunk( 
        "Auth/Logout",
        async (_, thunkAPI)=>{
            const {rejectedWithValue} = thunkAPI
            try{
                localStorage.removeItem("token")
                return
            }catch(err){
                return rejectedWithValue(err.message)
            }
        })

export const AuthRegister = createAsyncThunk( async (credentials, thunkAPI)=>{
    const {rejectedWithValue} = thunkAPI
    try{
        const response = await axios.post(
            server+"register", 
            credentials, 
            {
                headers:{"Content-Type":"application/json; charset=UTF-8"}
            })
        if(response.status === 200)
        {
            localStorage.setItem("token", response.data.token)
            return response.data
        }
        return rejectedWithValue(response.data)
        
    }catch(err){
        return rejectedWithValue(err.message)
    }
})

const authSlice = createSlice({
    name:"auth",
    initialState,
    reducers:{},
    extraReducers:builder=>{
        // LOGIN
        builder.addCase(AuthLogin.pending, (state, action)=>{
            state.IsLoading = true;
            state.IsAuthenticated = false;
        })
        builder.addCase(AuthLogin.fulfilled, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = true;
            state.token = String(action?.payload?.token)
            state.user = jwtDecode(action?.payload?.token);
        })
        builder.addCase(AuthLogin.rejected, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = false;
            state.error = action.payload
        })

        // LOGOUT
        builder.addCase(AuthLogout.pending, (state, action)=>{
            state.IsLoading = true;
            state.IsAuthenticated = true;
        })
        builder.addCase(AuthLogout.fulfilled, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = false;
            state.token = null
            state.user =null;
        })
        builder.addCase(AuthLogout.rejected, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = true;
            state.error = action.payload
        })


        // Register
        builder.addCase(AuthRegister.pending, (state, action)=>{
            state.IsLoading = true;
            state.IsAuthenticated = false;
        })
        builder.addCase(AuthRegister.fulfilled, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = true;
            state.token = action.payload
            state.user = jwtDecode(action.payload.access);
        })
        builder.addCase(AuthRegister.rejected, (state, action)=>{
            state.IsLoading = false;
            state.IsAuthenticated = false;
            state.error = action.payload
        })

    }
})

export default authSlice.reducer