<script>
    export let products = [
        {
            name: "Coca",
            buyingPrice: "7000",
            sellingPrice: "10000",
            quantity: 100,
        },
        {
            name: "Pepsi",
            buyingPrice: "8000",
            sellingPrice: "11000",
            quantity: 48,
        },
        {
            name: "Sprite",
            buyingPrice: "6000",
            sellingPrice: "9000",
            quantity: 72,
        }
    ];
</script>

<main>
    <h1>Minanis</h1>
    <table>
        <tr>
            <th>No.</th>
            <th>Name</th>
            <th>BuyingPrice</th>
            <th>SellingPrice</th>
            <th>Quantity</th>
        </tr>
        {#each products as product, index}
            <tr>
                <td>{index + 1}</td>
                <td>{product.name}</td>
                <td>{product.buyingPrice}</td>
                <td>{product.sellingPrice}</td>
                <td>{product.quantity}</td>
            </tr>
        {/each}
    </table>
</main>

<style>
    h1 {
        font-size: 2em;
        background: linear-gradient(to left, #30CFD0, cornflowerblue);
        -webkit-background-clip: text;
        color: transparent;
        width: fit-content;
        padding: 0;
        margin: 0 0 0.21em 0;
    }

    table {
        border: 2px solid black;
        border-collapse: collapse;
    }

    tr:nth-child(even) {
        background: powderblue;
    }

    th {
        border: 2px solid black;
        padding: 0.3em;
        margin: 2em;
        background: linear-gradient(to left, #30CFD0, cornflowerblue);
        -webkit-background-clip: text;
        color: transparent;
        width: 7em;
        text-align: left;
    }

    th:first-child {
        width: 3em;
    }
    th:nth-child(2) {
        width: 12em;
    }

    td {
        padding: 0.4em 0.3em;
    }
</style>