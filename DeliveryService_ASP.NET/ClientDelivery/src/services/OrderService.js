import {useHttp} from '../hooks/http.hook';

const useOrderService = () => {
    const {request, error, clearError} = useHttp();
    const _apiBase = 'https://localhost:7038/api/';

    const getToken = () => {
        const tokenString = sessionStorage.getItem('token');
        console.log(JSON.parse(tokenString));
        return JSON.parse(tokenString);

    }

    const getResource = async(url) => {
        return await request(url, 'GET', null, {'Authorization': 'Bearer ' + getToken()});
    }

    const registerUser = async(credentials) => {
        const url = `${_apiBase}auth/register`;
        return await request(url, 'POST', JSON.stringify(credentials), {'Content-Type': 'application/json'});
    }

    const loginUser = async(credentials) => {
        const url = `${_apiBase}auth/login`
        return await request(url, 'POST', JSON.stringify(credentials), {'Content-Type': 'application/json'});
    }

    const createOrder = async(data) => {
        const url = `${_apiBase}order/create`;
        return await request(url, 'POST', JSON.stringify(data), {'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getToken()})
    }

    const confirmOrder = async(data) => {
        const url = `${_apiBase}order/confirm`;
        return await request(url, 'POST', JSON.stringify(data), {'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getToken()})
    }

    const completeOrder = async(data) => {
        const url = `${_apiBase}order/complete`;
        return await request(url, 'POST', JSON.stringify(data), {'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getToken()})
    }

    const editCustomerProfile = async(data) => {
        const url = `${_apiBase}customer/editProfile`;
        return await request(url, 'POST', JSON.stringify(data), {'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + getToken()})
    }

    const getOrderById = async (id) => {
        const res = await getResource(_apiBase + `order/${id}`);
        console.log(res);
        return res;
    }

    const getCustomerById = async () => {
        const res = await getResource(`https://localhost:7038/api/customer/profile`);
        return res;
    }

    const getCourierById = async () => {
        const res = await getResource(`https://localhost:7038/api/courier/profile`);
        return res;
    }

    const getAllOrdersByCreate = async () => {
        const res = await getResource(_apiBase + `order/allOrdersByCreate`);
        return res;
    }

    const getAllOrdersByCustomer = async () => {
        const res = await getResource('https://localhost:7038/api/order/customerOrders');
        console.log(res);
        return res;
    }

    const getOrdersByCustomerByStatus = async(status) => {
        const res = await getResource(_apiBase + `order/customerOrders/${status}`);
        return res;
    }

    const getOrdersByCourierByOrderProgress = async() => {
        const res = await getResource(_apiBase + `order/courierOrders/Progress`);
        return res;
    }

    const getOrdersByCourierByOrderComplete = async() => {
        const res = await getResource(_apiBase + `order/courierOrders/Complete`);
        return res;
    }

    const getAllOrdersByCourier = async () => {
        const res = await getResource(_apiBase + `order/courierOrders`);
        return res;
    }

    const getAllProducts = async () => {
        const res = await getResource(_apiBase + `product/allProducts`);
        return res;
    }

    const getProductById = async (productId) => {
        const res = await getResource(_apiBase + `product/${productId}`);
        return res;
    }

    const getAllSections = async () => {
        const res = await getResource(_apiBase + `sections/allSections`);
        return res;
    }

    const getProductsBySectionId = async (sectionId) => {
        const res = await getResource(_apiBase + `product/allProducts/${sectionId}`);
        return res;
    }

    return {
        error, 
        clearError,
        getToken,
        getResource,
        registerUser,
        loginUser,
        createOrder,
        confirmOrder,
        completeOrder,
        getOrderById,
        getCustomerById,
        getCourierById,
        getAllOrdersByCreate,
        getAllOrdersByCustomer,
        getOrdersByCustomerByStatus,
        getOrdersByCourierByOrderProgress,
        getOrdersByCourierByOrderComplete,
        getAllOrdersByCourier,
        getProductsBySectionId,
        getAllSections,
        getAllProducts,
        getProductById,
        editCustomerProfile
    };
};

export default useOrderService;

