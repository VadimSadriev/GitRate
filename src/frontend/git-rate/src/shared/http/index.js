import axios from 'axios';
import { authFail } from '../../store/actions/auth';
import { push } from 'connected-react-router';

const httpConfig = {
    method: 'GET',
    baseURL: process.env.REACT_APP_API_URL,
    headers: {
        'Content-Type': 'application/json; charset=utf-8',
        'Accept': 'application/json'
    }
};

export const httpStatuses = {
    Unauthorized: 401,
    Forbidden: 403,
    NotFound: 404,
    InternalServerError: 500
}

function successRequestIntecepter(request) {
    const token = localStorage.getItem('token');
    if (token) {
        request.headers = { Authorization: 'Bearer ' + token };
    }

    return Promise.resolve(request);
}

function failureRequestIntecepter(error) {
    return Promise.reject(error);
}

export const http = axios.create(httpConfig);

http.interceptors.request.use(successRequestIntecepter, failureRequestIntecepter);

export const setupReduxResponseInterceptor = store => {
    http.interceptors.response.use(response => {
        return response;
    }, error => {
        if (error.response) {
            const { response } = error;

            switch (response.status) {
                case httpStatuses.Unauthorized:
                    store.dispatch(authFail());
                    store.dispatch(push('/signin'));
                    break;
                case httpStatuses.Forbidden:
                    break;
                case httpStatuses.GatewayTimeout:
                    break;
                case httpStatuses.InternalServerError:
                    break;
                case httpStatuses.NotFound:
                default:
                    break;
            }
        }
        return Promise.reject(error);
    })
}