import { useState } from "react";
import ToastContext from "./ToastService";
import { MdOutlineClose } from "react-icons/md";

export default function ToastProvider({children}){
    const [toasts, setToasts] = useState([])
    const open = (title, message, tag='info', timeout = 5000)=>{
        const id = Date.now()
        setToasts(toasts=> [...toasts, {id, title, message, tag}])
        setTimeout(()=>close(id), timeout)
    }


    const close = (id)=>setToasts(toasts.filter(i=>i.id !== id))
    
    const tags = {
        "error": "text-red-800 bg-red-300",
        "success": "text-green-800 bg-green-300",
    }


    return <ToastContext.Provider value={{open, close}}>
        {children}
        <div className="absolute space-y-2 bottom-4 right-4 w-[20%]">
            {
                toasts.map(({id, title, message, tag})=>(
                    <div className="relative" key={id}>
                        <button
                         onClick={()=>close(id)}
                         className="absolute top-2 right-2 p-1 rounded-lg bg-gray-200/20 text-gray-800/60">
                            <MdOutlineClose />
                        </button>
                        <div className={"flex gap-2 p-4 rounded-lg shadow-lg "+ tags[tag]}>
                            
                            <div className="">

                                <div className="font-bold">{title}</div>
                                <div className="text-sm">{message}</div>
                            </div>
                        </div>
                    </div>
                ))
            }
        </div>
    </ToastContext.Provider>
}