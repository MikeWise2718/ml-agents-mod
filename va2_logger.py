
import os
import glob
import sys
import tarfile
import time
from datetime import datetime
import json

clrNrm  = '\033[0m'  # white (normal)
clrR    = '\033[31m' # red
clrG    = '\033[32m' # green
clrY    = '\033[33m' # yellowe
clrB    = '\033[34m' # blue
clrP    = '\033[35m' # purple
clrC    = '\033[36m' # cyan
clrW    = '\033[37m' # yellown

clrIR="\033[0;91m"         # Red
clrIG="\033[0;92m"       # Green
clrIY="\033[0;93m"      # Yellow
clrIB="\033[0;94m"        # Blue
clrIP="\033[0;95m"      # Purple
clrIC="\033[0;96m"        # Cyan
clrIW="\033[0;97m"       # White


class lgger:

    verbosity = 3
    nodename = ""
    defnodecolor = clrNrm
    stripcolor = False
    l_err = 1
    l_warn = 2
    l_info = 3
    l_debug = 4

    def __init__(self, nodename,verbosity=1,defnodecolor=clrNrm):
        self.nodename = nodename
        self.verbosity = verbosity
        self.defnodecolor = defnodecolor

    def setverbosity(self,verbosity):
        self.verbosity = verbosity

    def dolog(self,lev,msg,clr=defnodecolor):
        if (lev<=self.verbosity):
            if (self.stripcolor):
                print(str(msg))
            else:
                print(clr+str(msg))
    
    def err(self,msg,clr=defnodecolor):
        self.dolog(self.l_err,msg,clr)

    def error(self,msg,clr=defnodecolor):
        self.dolog(self.l_err,msg,clr)

    def warn(self,msg,clr=defnodecolor):
        self.dolog(self.l_warn,msg,clr)

    def info(self,msg,clr=defnodecolor):
        self.dolog(self.l_info,msg,clr)

    def deb(self,msg,clr=defnodecolor):
        self.dolog(self.l_debug,msg,clr)

    def debug(self,msg,clr=defnodecolor):
        self.dolog(self.l_debug,msg,clr)

_lgg = None      

def createlgg(nodename,verbosity,defnodecolor):
    global _lgg
    _lgg = lgger(nodename,verbosity,defnodecolor)
    return _lgg

def getlgg():
    global _lgg
    if (_lgg==None):
        _lgg = lgger("nonname",3)
    return _lgg
