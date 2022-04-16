<script></script>

<ul>
    <li><a class="active" href="#home">Home</a></li>
    <li><a href="#user">User</a></li>
    <li><a href="#product">Product</a></li>
    <li><a href="#product-batch">ProductBatch</a></li>
    <li><a href="#order">Order</a></li>
</ul>

<style>
    ul {
        list-style-type: none;
        margin: 0;
        padding: 10px 0 0 0;
        width: 10%;
        min-width: 150px;
        background-color: powderblue;
        position: fixed;
        height: 100%;
        overflow: auto;
    }

    li a {
        display: block;
        padding: 8px 16px;
        font-weight: bold;
        text-decoration: none;
    }

    li a.active {
        background-color: deepskyblue;
        color: white;
    }

    li a:hover:not(.active) {
        background-color: deepskyblue;
        color: white;
    }
</style>