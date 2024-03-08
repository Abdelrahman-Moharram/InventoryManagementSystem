import React, { useEffect } from 'react'
import { useSelector } from 'react-redux'
import { useNavigate } from 'react-router-dom'

const IsAdmin = ({children}) => {
    let auth = useSelector((state)=>state.auth)
    const nav = useNavigate()

    useEffect(() => {
        if(!auth?.user?.roles?.toLowerCase().includes("admin")){
            return nav("/")
        }
      }, [auth.IsAuthenticated]);
    return(
        children 
    )
}


export default IsAdmin