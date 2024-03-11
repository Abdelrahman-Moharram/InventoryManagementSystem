import React, { useState } from 'react'
import { BiSolidDetail } from 'react-icons/bi';
import { FaEdit } from 'react-icons/fa';
import { SlOptionsVertical } from "react-icons/sl";
import { Link } from 'react-router-dom';
import { RiDeleteBin5Fill } from "react-icons/ri";

const Dropdown = ({id}) => {
    const [toggle, setToggle] = useState(false)
  return (
    <div className="">
            <button
             onClick={()=>setToggle(!toggle)}
             className="h-full p-2 text-gray-600 default-shadow-hover transition linear duration-300 rounded-full p-2">
                <SlOptionsVertical />
            </button>
        
        {
            toggle?
                <div
                    className="absolute end-0 z-10 mt-2 w-56 rounded-md border border-gray-100 bg-white shadow-lg"
                    role="menu"
                >
                    <div className="p-2">
                    <Link
                        to={id}
                        className="flex w-full items-center gap-2 rounded-lg px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 hover:text-gray-500"
                        role="menuitem"
                    >
                        <BiSolidDetail />
                        View Details
                    </Link>

                    <Link
                        to={`edit/${id}`}
                        className="flex w-full items-center gap-2 rounded-lg px-4 py-2 text-sm text-gray-700 hover:bg-gray-50 hover:text-gray-500"
                        role="menuitem"
                    >
                        <FaEdit />
                        Edit
                    </Link>

                    <Link
                        to={`delete/${id}`}
                        className="flex w-full items-center gap-2 rounded-lg px-4 py-2 text-sm text-red-700 hover:bg-red-50 hover:text-red-500"
                        role="menuitem"
                        >
                        <RiDeleteBin5Fill />
                        Delete
                        </Link>

                    
                    </div>
                </div>
            :null
        }
        </div>
  )
}

export default Dropdown