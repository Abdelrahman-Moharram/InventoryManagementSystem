import React from 'react'
import logo from '../../assets/logo.png'
import { Link, NavLink } from 'react-router-dom'

const SideNav = () => {
  return (
    <div className="flex h-screen flex-col justify-between border-e bg-white">
        <div className="px-4 py-6">
            <Link to={"/admin"} className="grid h-10 w-32 place-content-center rounded-lg bg-gray-100 text-xs text-gray-600">
                <img
                    className="h-8 w-auto"
                    src={logo}
                    alt="Your Company"
                />
            </Link>

            <ul className="mt-6 space-y-1">
            <li>
                <NavLink
                to="/admin"
                className="block rounded-lg bg-gray-100 px-4 py-2 text-sm font-medium text-gray-700"
                >
                General
                </NavLink>
            </li>

            

            <li>
                <NavLink
                to="products"
                className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                >
                Products
                </NavLink>
            </li>

            <li>
                <Link
                to="category"
                className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                >
                Category
                </Link>
            </li>
            <li>
                <Link
                to="Brands"
                className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                >
                Brands
                </Link>
            </li>
            
            <li>
                <details className="group [&_summary::-webkit-details-marker]:hidden">
                <summary
                    className="flex cursor-pointer items-center justify-between rounded-lg px-4 py-2 text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                >
                    <span className="text-sm font-medium"> Auth </span>

                    <span className="shrink-0 transition duration-300 group-open:-rotate-180">
                    <svg
                        xmlns="http://www.w3.org/2000/svg"
                        className="h-5 w-5"
                        viewBox="0 0 20 20"
                        fill="currentColor"
                    >
                        <path
                        fillRule="evenodd"
                        d="M5.293 7.293a1 1 0 011.414 0L10 10.586l3.293-3.293a1 1 0 111.414 1.414l-4 4a1 1 0 01-1.414 0l-4-4a1 1 0 010-1.414z"
                        clipRule="evenodd"
                        />
                    </svg>
                    </span>
                </summary>

                <ul className="mt-2 space-y-1 px-4">
                    <li>
                    <NavLink
                        to="#"
                        className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                    >
                        Users
                    </NavLink>
                    </li>

                    <li>
                    <NavLink
                        to="auth/roles"
                        className="block rounded-lg px-4 py-2 text-sm font-medium text-gray-500 hover:bg-gray-100 hover:text-gray-700"
                    >
                        Roles
                    </NavLink>
                    </li>
                </ul>
                </details>
            </li>
            </ul>
        </div>

        <div className="sticky inset-x-0 bottom-0 border-t border-gray-100">
            <Link to="#" className="flex items-center gap-2 bg-white p-4 hover:bg-gray-50">
            <img
                alt=""
                src="https://images.unsplash.com/photo-1600486913747-55e5470d6f40?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1770&q=80"
                className="size-10 rounded-full object-cover"
            />

            <div>
                <p className="text-xs">
                <strong className="block font-medium">Eric Frusciante</strong>

                <span> eric@frusciante.com </span>
                </p>
            </div>
            </Link>
        </div>
    </div>
  )
}

export default SideNav