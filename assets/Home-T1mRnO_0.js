import{c,a as e,o,d as w,t as v,_ as I,n as T,u as _,b as d,r as x,w as C,S as $,e as A,F as S,f as y,g as k,v as N,h as O,i as U}from"./index-DAANOoOM.js";const z={xmlns:"http://www.w3.org/2000/svg",width:"24",height:"24",fill:"currentColor"};function B(l,s){return o(),c("svg",z,s[0]||(s[0]=[e("path",{d:"m14.829 7.757-5.657 5.657a1 1 0 1 0 1.414 1.414l5.657-5.656A3 3 0 0 0 12 4.929l-5.657 5.657a5 5 0 0 0 7.071 7.07L19.071 12l1.414 1.414-5.656 5.657a7 7 0 0 1-9.9-9.9l5.657-5.656a5 5 0 0 1 7.071 7.07L12 16.244A3 3 0 0 1 7.758 12l5.656-5.657z"},null,-1)]))}const D={render:B},H={xmlns:"http://www.w3.org/2000/svg",width:"24",height:"24",fill:"currentColor"};function L(l,s){return o(),c("svg",H,s[0]||(s[0]=[e("path",{d:"m20.713 8.128-.246.566a.506.506 0 0 1-.934 0l-.246-.566a4.36 4.36 0 0 0-2.22-2.25l-.759-.339a.53.53 0 0 1 0-.963l.717-.319a4.37 4.37 0 0 0 2.251-2.326l.253-.611a.506.506 0 0 1 .942 0l.253.61a4.37 4.37 0 0 0 2.25 2.327l.718.32a.53.53 0 0 1 0 .962l-.76.338a4.36 4.36 0 0 0-2.219 2.251M12 2C6.477 2 2 6.477 2 12c0 1.703.425 3.306 1.176 4.709L2 22l5.291-1.176A9.96 9.96 0 0 0 12 22c5.523 0 10-4.477 10-10q0-.604-.07-1.19l-1.986.235q.056.47.056.955a8 8 0 0 1-8 8 7.96 7.96 0 0 1-3.766-.94l-.653-.349-2.947.655.655-2.947-.35-.653A7.96 7.96 0 0 1 4 12a8 8 0 0 1 10-7.748l.498-1.937C13.698 2.109 12.861 2 12 2M9 12H7a5 5 0 0 0 10 0h-2a3 3 0 1 1-6 0"},null,-1)]))}const E={render:L},j={xmlns:"http://www.w3.org/2000/svg",width:"24",height:"24",fill:"currentColor"};function q(l,s){return o(),c("svg",j,s[0]||(s[0]=[e("path",{d:"M3 13h6v-2H3V1.846a.5.5 0 0 1 .741-.439l18.462 10.155a.5.5 0 0 1 0 .876L3.741 22.592A.5.5 0 0 1 3 22.154z"},null,-1)]))}const V={render:q},F={class:"avatar"},J={key:0,class:"avatar-img"},K=["src"],G={key:1,class:"avatar-noicon"},P=w({__name:"Avatar",props:{img:{},text:{}},setup(l){return(s,t)=>{var a;return o(),c("div",F,[s.img?(o(),c("div",J,[e("img",{src:s.img},null,8,K)])):(o(),c("div",G,v((a=s.text)==null?void 0:a.split("")[0]),1))])}}}),M=I(P,[["__scopeId","data-v-9402bc13"]]),R={class:"message-user__item"},W={class:"item-text"},Q={class:"message-user__text"},X={class:"item-time"},Y=w({__name:"Message",props:{message:{},members:{}},setup(l){const s=l,t=Number(document.cookie.split("=")[1]),a=()=>{const r=s.message.CreatedDateTime.split("T")[1].split(":")[0],n=s.message.CreatedDateTime.split("T")[1].split(":")[1];return`${r}:${n}`};return(r,n)=>(o(),c("div",{class:T(["message-user",r.message.UserId===_(t)?"user-friend":""])},[d(M,{text:r.members[r.message.UserId]},null,8,["text"]),e("div",R,[e("div",W,[e("p",Q,v(r.message.Text),1)]),e("p",X,v(a()),1)])],2))}}),Z=I(Y,[["__scopeId","data-v-cfa87047"]]),ss="/OctopusChat/assets/user_icon-o50q4ITU.jfif",es={xmlns:"http://www.w3.org/2000/svg",width:"20",height:"20",fill:"currentColor",viewBox:"0 0 24 24"};function ts(l,s){return o(),c("svg",es,s[0]||(s[0]=[e("path",{d:"m18.031 16.617 4.283 4.282-1.415 1.415-4.282-4.283A8.96 8.96 0 0 1 11 20c-4.968 0-9-4.032-9-9s4.032-9 9-9 9 4.032 9 9a8.96 8.96 0 0 1-1.969 5.617m-2.006-.742A6.98 6.98 0 0 0 18 11c0-3.867-3.133-7-7-7s-7 3.133-7 7 3.133 7 7 7a6.98 6.98 0 0 0 4.875-1.975z"},null,-1)]))}const as={render:ts},ns={class:"chats"},os={class:"chats-avatar"},rs={for:"search",class:"chats-avatar__search"},cs={class:"chats-users"},ls={class:"chats-users__user chats-active"},is={class:"user-name"},ds={class:"name"},us={class:"last-message"},ms=w({__name:"Sidebar",props:{messages:{}},setup(l){const s=l,t=document.cookie.split("=")[1],a=x(""),r=()=>{var n,i,u;return(u=(i=s.messages)==null?void 0:i.Messages[((n=s.messages)==null?void 0:n.Messages.length)-1])==null?void 0:u.Text};return C(()=>s.messages,n=>{n&&(a.value=n.Members[t==="4"?"3":"4"])}),(n,i)=>{var u;return o(),c("aside",ns,[e("div",os,[d(M,{text:(u=n.messages)==null?void 0:u.Members[_(t)]},null,8,["text"]),e("label",rs,[d(_(as)),i[0]||(i[0]=e("input",{type:"search",name:"search",id:"search",placeholder:"Search..."},null,-1))])]),i[1]||(i[1]=e("p",{class:"chats-title"},"Чаты",-1)),e("ul",cs,[e("li",ls,[d(M,{img:_(ss),text:a.value},null,8,["img","text"]),e("div",is,[e("h4",ds,v(a.value),1),e("p",us,v(r()),1)])])])])}}}),_s={class:"chat"},hs={class:"chat-messages"},ps={class:"chat-messages__message"},gs={class:"chat-messages__input"},vs={class:"input-wrap"},fs=w({__name:"Home",setup(l){const s=x(""),t=x({Members:{},Messages:[]}),a=sessionStorage.getItem($.NAME_TOKEN),r=Number(document.cookie.split("=")[1]),n=O(),i=async()=>{const f=await fetch(`https://${$.HOST}/chat/1`,{method:"GET",headers:{Authorization:`Bearer ${a}`},credentials:"include"});t.value=await f.json()},u=async()=>{var p,m,g;if(s.value===null||s.value==="")return;const f=(m=t.value)==null?void 0:m.Messages[((p=t.value)==null?void 0:p.Messages.length)-1].Id;console.log(r);const h={Id:f,Text:s.value,CreatedDateTime:new Date().toISOString(),ChatId:1,UserId:r};await fetch(`https://${$.HOST}/chat/send`,{method:"POST",headers:{Accept:"application/json","Content-Type":"application/json",Authorization:`Bearer ${a}`},credentials:"include",body:JSON.stringify(h)}),(g=t.value)==null||g.Messages.push(h),s.value=""},b=()=>{a||n.push("authorize")};return A(()=>{b(),i()}),(f,h)=>{var p;return o(),c("div",_s,[d(ms,{messages:t.value},null,8,["messages"]),e("div",hs,[e("div",ps,[(o(!0),c(S,null,y((p=t.value)==null?void 0:p.Messages,m=>{var g;return o(),U(Z,{key:m.Id,message:m,members:(g=t.value)==null?void 0:g.Members},null,8,["message","members"])}),128))]),e("div",gs,[e("div",vs,[d(_(D),{class:"input-wrap__clip"}),k(e("input",{type:"text",name:"message",class:"messages-input",placeholder:"Написать сообщение...",maxlength:"512","onUpdate:modelValue":h[0]||(h[0]=m=>s.value=m)},null,512),[[N,s.value]]),d(_(E),{class:"input-wrap__smile"}),e("button",{class:"btn",onClick:u},[d(_(V),{class:"input-wrap__send"})])])])])])}}}),$s=I(fs,[["__scopeId","data-v-1511ca3f"]]);export{$s as default};
