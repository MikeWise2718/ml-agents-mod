
import os
import glob
import sys
import tarfile
import time
from colorama import init
init()

from datetime import datetime
import json
# __all__ = ['lgg','clrNrm',
#           'clrR','clrG','clrY','clrB','clrP','clrM','clrC','clrW',
#           'clrIR','clrIG','clrIY','clrIB','clrIP','clrIM','clrIC','clrIW',
#           ]

clrNrm  = '\033[0m'  # white (normal)
clrR    = '\033[31m' # red
clrG    = '\033[32m' # green
clrY    = '\033[33m' # yellowe
clrB    = '\033[34m' # blue
clrP    = '\033[35m' # purple
clrM    = '\033[35m' # purple
clrC    = '\033[36m' # cyan
clrW    = '\033[37m' # yellown

clrIR="\033[0;91m"         # Red
clrIG="\033[0;92m"       # Green
clrIY="\033[0;93m"      # Yellow
clrIB="\033[0;94m"        # Blue
clrIP="\033[0;95m"      # Purple
clrIM="\033[0;95m"      # Purple
clrIC="\033[0;96m"        # Cyan
clrIW="\033[0;97m"       # White

class Lgger:

    verbosity = 3
    nodename = ""
    defnodecolor = clrNrm
    stripcolor = False
    l_err = 1
    l_warn = 2
    l_info = 3
    l_debug = 4

    def __init__(self, nodename="default",verbosity=3,defnodecolor=clrNrm):
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
                print(clr+str(msg)+clrNrm)
    
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

    def GetLogger(self):
        return getlgg()

lgg = Lgger("nonname2",3)    
print("Initialized lgg")

