/*levi i desni sidebar*/

var mini = true;

function toggleSidebarLeft() {
    if (mini) {
        document.getElementById("mySidebarLeft").style.width = "270px";
        this.mini = false;
    } else {
        document.getElementById("mySidebarLeft").style.width = "115px";
        this.mini = true;
    }
}

var mini = true;

function toggleSidebarRight() {
    if (mini) {
        document.getElementById("mySidebarRight").style.width = "270px";
        this.mini = false;
    } else {
        document.getElementById("mySidebarRight").style.width = "115px";
        this.mini = true;
    }
}
