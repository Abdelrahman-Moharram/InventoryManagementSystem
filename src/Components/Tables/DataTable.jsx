import React from 'react'
import DefaultOptions from '../Others/DefaultOptions';

const DataTable = ({data, keys, keyfilter, options=true}) => {

  return (


    <div className="overflow-x-auto rounded-lg border border-gray-200">
        <table className="min-w-full divide-y-2 divide-gray-200 bg-white text-sm md:text-xs">
            <thead className="ltr:text-left rtl:text-right">
            <tr>
                {keys.map(key=><th className=" py-2 text-center" key={key}>{key}</th>)}
                {
                    options?
                    <>
                        <th className="text-center py-2">options</th>
                    </>:null
                }
            </tr>
            </thead>

            <tbody className="divide-y divide-gray-200">
                {
                    data?.map(item=>(
                    <tr className='odd:bg-gray-50' key={item.id}>
                        {keyfilter.map(k=>(
                            k.includes("color") ?
                            <td key={item.id+k} className="whitespace-nowrap  py-2 font-medium text-gray-900 flex">
                                {
                                    item[k]?
                                    item[k].split(",").map(color=>
                                        <div key={color} className={`mx-auto rounded-full w-[15px] gap-2 border border-black-500 h-[15px]`} style={{backgroundColor:color}}></div>
                                    ):null
                                }
                            </td>
                            :
                            <td key={item.id+k} className="whitespace-nowrap  py-2 font-medium text-gray-900 text-center">{item[k]?item[k]:" - "}</td>
                            )
                        
                        )}
                        {
                            options?
                            <>
                            
                            <td className="whitespace-nowrap text-center py-2">
                                <DefaultOptions id={item.id} />
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