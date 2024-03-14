import React, { useEffect } from 'react'
import { useNavigate, useParams } from 'react-router-dom'
import { useToast } from '../../../store/Toast/ToastService'
import { DeleteProduct } from '../../../Api/Products'

const Delete = () => {

    const nav = useNavigate()
    const {id} = useParams()
    const toast = useToast()

    useEffect(()=>{
        console.log(id);
        DeleteProduct(id)
            .then(res=>{
                if(res.status === 200){
                    toast.open("Success", res.data,'success')
                    nav("/admin/products")
                }else{
                    toast.open("Error", res.data,'error')
                }
            }).catch(err=>{
                toast.open("Error", err.message,'error')
                nav("/admin/products")
            })
    }, [id])
    
    return (
        <div>DeleteProduct</div>
    )
}

export default Delete