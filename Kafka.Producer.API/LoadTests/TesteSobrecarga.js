import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '10s', target: 50 },   // sobe carga
        { duration: '20s', target: 200 },  // pico
        { duration: '20s', target: 200 },  // sustenta
        { duration: '10s', target: 0 }     // desacelera
    ],

    thresholds: {
        http_req_failed: ['rate<0.01'], // menos de 1% de erro
        http_req_duration: ['p(95)<500'] // 95% < 500ms
    }
};

export default function () {

    const payload = JSON.stringify({
        nome: `User-${__VU}-${__ITER}`,
        idade: Math.floor(Math.random() * 60 + 18)
    });

    const params = {
        headers: {
            'Content-Type': 'application/json'
        }
    };

    const res = http.post('https://localhost:5001/', payload, params);

    // valida resposta da API
    check(res, {
        'status é 200': (r) => r.status === 200,
    });

    sleep(0.1); // simula comportamento real de usuário
}