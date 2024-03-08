import React from 'react'
import { Link } from 'react-router-dom';
import { FaEdit } from "react-icons/fa";
import { BiSolidDetail } from "react-icons/bi";
import { MdDeleteOutline } from "react-icons/md";

const DataTable = ({data, keys, keyfilter, options=true}) => {

  return (


    <div className="overflow-x-auto">
    <table className="min-w-full divide-y-2 divide-gray-200 bg-white text-sm">
        <thead className="ltr:text-left rtl:text-right">
        <tr>
            {keys.map(key=><th className="px-4 py-2 text-left" key={key}>{key}</th>)}
            {
                options?
                <>
                    <th className="px-4 py-2"></th>
                    <th className="px-4 py-2"></th>
                    <th className="px-4 py-2"></th>
                </>:null
            }
        </tr>
        </thead>

        <tbody className="divide-y divide-gray-200">
            {
                data?.map(item=>(
                <tr key={item.id}>
                    {keyfilter.map(k=>(
                        k.includes("color") ?
                        <td key={item.id+k} className="whitespace-nowrap px-4 py-2 font-medium text-gray-900 flex">
                            {
                                item[k]?
                                item[k].split(",").map(color=>
                                    <div key={color} className={`mx-auto rounded-full w-[15px] border border-black-500 h-[15px]`} style={{backgroundColor:color}}></div>
                                ):null
                            }

                        </td>
                        :
                        <td key={item.id+k} className="whitespace-nowrap px-4 py-2 font-medium text-gray-900">{item[k]}</td>

                        )
                    
                    )}
                    {
                        options?
                        <>
                            <td className="whitespace-nowrap px-4 py-2">
                            <Link to={item.id} title='details' className="inline-block rounded bg-[#111] px-4 py-2 text-xs font-medium text-white hover:bg-[#444]"><BiSolidDetail /></Link>
                        </td>
                        <td className="whitespace-nowrap px-4 py-2">
                            <Link to={"edit/"+item.id}  title='edit' className="inline-block rounded bg-blue-700 px-4 py-2 text-xs font-medium text-white hover:bg-blue-500"><FaEdit /></Link>
                        </td>
                        <td className="whitespace-nowrap px-4 py-2">
                            <Link to={"delete/"+item.id} title='delete' className="inline-block rounded bg-red-600 px-4 py-2 text-xs font-medium text-white hover:bg-red-500"><MdDeleteOutline /></Link>
                        </td>
                        </>
                        :null
                    }
                </tr>
                )
                    
        )
    }
        </tbody>
    </table>
    </div>
  )
}

export default DataTable