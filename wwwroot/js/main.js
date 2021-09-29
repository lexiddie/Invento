function handleToggle(index) {
    const handleList = ['stock-management', 'inventory-management'];
    const x = document.getElementById(handleList[index]);
    if (x.className.indexOf('nav-expand') === -1) {
        x.className += ' nav-expand';
    } else {
        x.className = x.className.replace(' nav-expand', '');
    }
}

