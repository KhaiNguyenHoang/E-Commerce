import React from "react";
import { Link } from "react-router-dom";
import ProductCard from "../components/ProductCard.jsx";

function ProductPage() {
    return(
        <div className="container mx-auto p-4">
            <h1 className="text-3xl font-extrabold text-gray-800 mb-6">Product List</h1>
            <div className="flex justify-end mb-4">
                <Link to="/add">
                    <button className="bg-indigo-600 hover:bg-indigo-700 text-white font-semibold py-2 px-6 rounded-lg shadow-md transition duration-300 ease-in-out">
                        Add New Product
                    </button>
                </Link>
            </div>
            <div className="bg-white p-6 rounded-lg shadow-lg">
                <ProductCard />
            </div>
        </div>
    )
}

export default ProductPage;